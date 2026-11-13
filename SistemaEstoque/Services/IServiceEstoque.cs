using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.Model.Entidades;
using SistemaEstoque.Model.Enum;

namespace SistemaEstoque.Services
{
    public interface IServiceEstoque
    {
        Task<object> PostCadastrarProduto(string nome, double precoCusto, int quantidade, Categorias categorias, double precoVenda, int estoqueMinimo, string CnpjFornecedor);
        Task<object> PutEditarProduto(int id, double precoCusto, double precoVenda, string CnpjFornecedor);
        Task<object> GetConsultarProduto(int id);
        Task<List<object>> GetConsultarTodosOSProdutos();
        Task<String> PutRegistrarSaida(int id, int quantidade);
        Task<String> PutRegistrarEntrada(int id, int quantidade);
        Task<byte[]> GetRelatorioDisposição();
        Task<byte[]> GetMovimentação();
        Task<string> PostCadastrarFornecedor(Fornecedor fornecedor);
        Task<object> GetFornecedores();

    }
}
