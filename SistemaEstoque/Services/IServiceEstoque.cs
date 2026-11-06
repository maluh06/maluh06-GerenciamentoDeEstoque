using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.Model.Entidades;

namespace SistemaEstoque.Services
{
    public interface IServiceEstoque
    {
        Task<object> PostCadastrarProduto(AdicionarProduto produto);
        Task<String> PutEditarProduto(int id, double precoCusto, double precoVenda, Fornecedor fornecedor);
        Task<object> GetConsultarProduto(int id);
        Task<List<object>> GetConsultarTodosOSProdutos();
        Task<String> PutRegistrarSaida(int id, int quantidade);
        Task<String> PutRegistrarEntrada(int id, int quantidade);
        /*Task<ActionResult<Produto>> GetRelatorioDisposição();
        Task<ActionResult<Produto>> GetMovimentação(DateTime periodoInicial);*/
    }
}
