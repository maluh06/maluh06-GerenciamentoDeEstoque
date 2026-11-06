using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SistemaEstoque.Model.ApplicationDbContext;
using SistemaEstoque.Model.Entidades;

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
            Produto novoProduto = new AdicionarProduto
            {
                nome = produto.nome,
                precoCusto = produto.precoCusto,
                quantidade = produto.quantidade,
                categoria = produto.categoria,
                precoVenda = produto.precoVenda,
                estoqueMinimo = produto.estoqueMinimo,
                fornecedor = produto.fornecedor
                //concertar essa parte de adicionar fornecedor
            };

            await _context.Produtos.AddAsync(novoProduto);
            await _context.SaveChangesAsync();

            return novoProduto.GerarMensagem(); 
        }
        public async Task<String> PutEditarProduto(int id, double precoCusto, double precoVenda, Fornecedor fornecedor)
        {
            var editar = await _context.Produtos.FindAsync(id);
            editar.precoCusto = precoCusto;
            editar.precoVenda = precoVenda;
            editar.fornecedor = fornecedor;
            editar.ultimaEntrada = DateTime.Now;
            await _context.SaveChangesAsync();

            return editar.GerarMensagem() + "\n Ultima Entrada: " + editar.ultimaEntrada;
        }

        public async Task<String> PutRegistrarEntrada(int id, int quantidade)
        {
            var entrada = await _context.Produtos.FindAsync(id);

            entrada.quantidade += quantidade;
            entrada.ultimaEntrada = DateTime.Now;
            await _context.SaveChangesAsync();

            return "Nome do produto: " + entrada.nome + "\nQuantidade: " + entrada.quantidade + "\nUnidades repostas: " + quantidade;
        }

        public async Task<String> PutRegistrarSaida(int id, int quantidade)
        {
            var saida = await _context.Produtos.FindAsync(id);
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

        /*public async Task<ActionResult<Prduto>> GetMovimentação(DateTime periodoInicial)
       {

           // Buscaremos todas as movimentações de entrada e saída de produtos no período informado até o periodo atual
           // ver sobre estabelecer uma formatação de data 
           // gerar um arquivo
       }

       public async Task<ActionResult<Produto>> GetRelatorioDisposição()
       {
           // Ter no arquivo gerado itens: 
           //                             acima do estoque mínimo, abaixo do estoque mínimo e produtos com estoque zerado.
           // mostrará nome quantidade, status (acima do estoque mínimo, abaixo do estoque mínimo e produtos com estoque zerado)
       }*/
    }
}
