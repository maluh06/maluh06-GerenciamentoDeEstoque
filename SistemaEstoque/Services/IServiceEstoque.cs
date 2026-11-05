using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.Model.Entidades;

namespace SistemaEstoque.Services
{
    public interface IServiceEstoque
    {
        Task<Produto> PostCadastrarProduto(AdicionarProduto produto);
        Task<Produto> PutEditarProduto(int id, double precoCusto, double precoVenda, Fornecedor fornecedor);
        Task<Produto> GetConsultarProduto(int id);
        Task<List<Produto>> GetConsultarTodosOSProdutos();
        Task<Produto> PutRegistrarSaida(int id, int quantidade);
        Task<Produto> PutRegistrarEntrada(int id, int quantidade);
        /*Task<ActionResult<Produto>> GetRelatorioDisposição();
        Task<ActionResult<Produto>> GetMovimentação(DateTime periodoInicial);*/
    }
}
