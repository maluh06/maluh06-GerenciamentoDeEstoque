using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SistemaEstoque.Model.ApplicationDbContext;
using SistemaEstoque.Model.Entidades;
using SistemaEstoque.Model.Enum;
using System.Collections;

namespace SistemaEstoque.Services
{
    public class ServiceEstoque : IServiceEstoque
    {
        private readonly Context _context;


        public ServiceEstoque(Context context)
        {
            _context = context;
        }

        public async Task<object> GetConsultarProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            return produto.GerarMensagem();
        }

        public async Task<List<object>> GetConsultarTodosOSProdutos()
        {
            var produtos = await _context.Produtos.ToListAsync();
            return produtos.Select(p => p.GerarMensagem()).ToList();
        }

        public async Task<object> PostCadastrarProduto(AdicionarProduto produto)
        {
            var validarFornecedor = await _context.Fornecedores.FindAsync(produto.CnpjFornecedor);

            if (validarFornecedor != null)
            {
                Produto novoProduto = new AdicionarProduto
                {
                    nome = produto.nome,
                    precoCusto = produto.precoCusto,
                    quantidade = produto.quantidade,
                    categoria = produto.categoria,
                    precoVenda = produto.precoVenda,
                    estoqueMinimo = produto.estoqueMinimo,
                    CnpjFornecedor = produto.CnpjFornecedor

                };

                await _context.Produtos.AddAsync(novoProduto);
                await _context.SaveChangesAsync();

                return novoProduto.GerarMensagem();
            }

            else{
                return new { Mensagem = "Fornecedor não encontrado. Certifique-se de que o CNPJ do fornecedor está correto ou cadastre esse fornecedor antes de seguir com a adição do produto." };
            }
        }

        public async Task<string> PostCadastrarFornecedor(Fornecedor fornecedor)
        {
            var validarFornecedor = await _context.Fornecedores.AddAsync(fornecedor);
            await _context.SaveChangesAsync();

            return "Fornecedor cadastrado com sucesso. \n\nCNPJ: " + fornecedor.CNPJ + "\nNome: " + fornecedor.Nome;
        }
      
        public async Task<object> GetFornecedores()
        {
            var fornecedores = await _context.Fornecedores.ToListAsync();
            return fornecedores.Select(f => f.GerarMensagem()).ToList();
        }

        public async Task<object> PutEditarProduto(int id, double precoCusto, double precoVenda, string CnpjFornecedor)
        {
            var editar = await _context.Produtos.FindAsync(id);
            var validarFornecedor = await _context.Fornecedores.FindAsync(editar.CnpjFornecedor);


            if (validarFornecedor != null)
            {
                editar.precoCusto = precoCusto;
                editar.precoVenda = precoVenda;
                editar.CnpjFornecedor = CnpjFornecedor;
                editar.ultimaEntrada = DateTime.Now;
                await _context.SaveChangesAsync();

                return editar.GerarMensagem() + "\n Ultima Entrada: " + editar.ultimaEntrada;
            }

            else
            {
                return new { Mensagem = "Fornecedor não encontrado. Certifique-se de que o CNPJ do fornecedor está correto ou cadastre esse fornecedor antes de seguir com a adição do produto." };
            }
        }

        public async Task<String> PutRegistrarEntrada(int id, int quantidade)
        {
            var entrada = await _context.Produtos.FindAsync(id);

            var movimentacao = await _context.Movimentacoes.AddAsync(new Movimentacoes
            {
                nomeProduto = entrada.nome,
                idProduto = entrada.id,
                quantTotal = entrada.quantidade + quantidade,
                UnidadeMovimentada = quantidade,
                tipoMovimentacao = Model.Enum.TipoMovimentacao.Entrada,
                dataEntrada = DateTime.Now
            });

            entrada.quantidade += quantidade;
            entrada.ultimaEntrada = DateTime.Now;
            await _context.SaveChangesAsync();

            return "Nome do produto: " + entrada.nome + "\nQuantidade: " + entrada.quantidade + "\nUnidades repostas: " + quantidade;
        }

        public async Task<String> PutRegistrarSaida(int id, int quantidade)
        {
            var saida = await _context.Produtos.FindAsync(id);

            var movimentacao = await _context.Movimentacoes.AddAsync(new Movimentacoes
            {
                nomeProduto = saida.nome,
                idProduto = saida.id,
                quantTotal = saida.quantidade - quantidade,
                UnidadeMovimentada = quantidade,
                tipoMovimentacao = Model.Enum.TipoMovimentacao.Saida,
                dataEntrada = DateTime.Now
            });

            var subtrair = saida.quantidade - quantidade;
            saida.ultimaEntrada = DateTime.Now;

            if (subtrair <= saida.estoqueMinimo && subtrair != 0 && subtrair > 0 )
            {
                saida.quantidade -= quantidade;
                await _context.SaveChangesAsync();
                return "Esse produto se encontra com o seu estoque baixo. Atualmente há " + saida.quantidade + " unidades em estoque.";
            }
            else if (subtrair <= 0)
            {
                return "Estoque insuficiente para essa retirada. Atualmente há " + saida.quantidade + " unidades em estoque.";
            }

            saida.quantidade -= quantidade;
            await _context.SaveChangesAsync();
            return "Nome do produto: " + saida.nome + "\nQuantidade: " + saida.quantidade + "\nUnidades retiradas: " + quantidade;
        }

        public async Task<byte[]> GetMovimentação()
       {
            var movimentacoes = await _context.Movimentacoes.ToListAsync();
            string titulo = "RELATÓRIO DE MOVIMENTAÇÕES DE PRODUTOS\n\n";

            foreach (var movimentacao in movimentacoes)
            {
                titulo += movimentacao.GerarMensagem() + "\n\n";
            }

            byte[] arquivo = System.Text.Encoding.UTF8.GetBytes(titulo);
            return arquivo;
        }

        public async Task<byte[]> GetRelatorioDisposição()
       {
            var produtos = await _context.Produtos.ToListAsync();
            string titulo = "RELATÓRIO SOBRE A SITUAÇÃO DO ESTOQUE (" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ")\n\n";

            foreach (var produto in produtos)
            {
                if(produto.estoqueMinimo >= produto.quantidade)
                {
                    titulo += "Produto: " + produto.nome + "\nQuantidade: " + produto.quantidade + "\nStatus: Abaixo do estoque mínimo\n\n";
                }
                else if (produto.quantidade == 0)
                {
                    titulo += "Produto: " + produto.nome + "\nQuantidade: " + produto.quantidade + "\nStatus: Estoque zerado\n\n";
                }
                else
                {
                    titulo += "Produto: " + produto.nome + "\nQuantidade: " + produto.quantidade + "\nStatus: Acima do estoque mínimo\n\n";
                }
            }

            byte[] arquivo = System.Text.Encoding.UTF8.GetBytes(titulo);
            return arquivo;
        }
    }
}
