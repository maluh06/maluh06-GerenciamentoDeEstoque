using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Produto> GetConsultarProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            //criar tratativa caso o ID não exista, com frase personalizada
            // criar uma mensagem para exbir os dados do produto (override)
            return produto;
        }

        public async Task<List<Produto>> GetConsultarTodosOSProdutos()
        {
            var produtos = await _context.Produtos.ToListAsync();
            //criar tratativa caso não tenha nenhum produto cadastrado, com frase personalizada
            // criar uma mensagem para exbir os dados de cada produto (override)

            return produtos;
        }

        public async Task<Produto> PostCadastrarProduto(AdicionarProduto produto)
        {
            var novoProduto = new Produto
            {
                nome = produto.nome,
                precoCusto = produto.precoCusto,
                quantidade = produto.quantidade,
                categoria = produto.categoria,
                precoVenda = produto.precoVenda,
                estoqueMinimo = produto.estoqueMinimo,
                primeiraEntrada = DateTime.Now,
                fornecedor = produto.fornecedor
            };

            await _context.Produtos.AddAsync(novoProduto);
            // criar uma mensagem para exbir os dados do produto (override)
            await _context.SaveChangesAsync();

            return novoProduto;
        }
        public async Task<Produto> PutEditarProduto(int id, double precoCusto, double precoVenda, Fornecedor fornecedor)
        {
            var editar = await _context.Produtos.FindAsync(id);
            //criar tratativa caso o ID não exista, com frase personalizada
            // criar uma mensagem para exbir os dados do produto (override)

            editar.precoCusto = precoCusto;
            editar.precoVenda = precoVenda;
            editar.fornecedor = fornecedor;
            editar.ultimaEntrada = DateTime.Now;
            await _context.SaveChangesAsync();

            return editar;
        }

        public async Task<Produto> PutRegistrarEntrada(int id, int quantidade)
        {
            var entrada = await _context.Produtos.FindAsync(id);
            //criar tratativa caso o ID não exista, com frase personalizada
            // criar uma mensagem para exbir os dados do produto (override) exibindo nome do produto e sua quantidade


            entrada.quantidade += quantidade;
            entrada.ultimaEntrada = DateTime.Now;
            await _context.SaveChangesAsync();

            return entrada;
        }

        public async Task<Produto> PutRegistrarSaida(int id, int quantidade)
        {
            var saida = await _context.Produtos.FindAsync(id);
            //criar tratativa caso o ID não exista, com frase personalizada
            // criar uma mensagem para exbir os dados do produto (override) exibindo nome do produto e sua quantidade
            //criar o alerta de estoque mínimo 
            //criar uma mensagem quando o estoque tiver zerado ou querer tirar uma quantidade que não tem suficiente em estoque

            saida.quantidade -= quantidade;
            saida.ultimaEntrada = DateTime.Now;
            await _context.SaveChangesAsync();
            return saida;
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
