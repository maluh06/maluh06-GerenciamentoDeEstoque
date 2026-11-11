using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Model.ApplicationDbContext;
using SistemaEstoque.Model.Entidades;
using SistemaEstoque.Services;

namespace SistemaEstoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstoqueController : Controller
    {
            private readonly ILogger<EstoqueController> _logger;
            private readonly Context context;
            private readonly ServiceEstoque serviceEstoque;
            public EstoqueController(Context context, ILogger<EstoqueController> logger, ServiceEstoque serviceEstoque)
            {
                this.context = context;
                this._logger = logger;
                this.serviceEstoque = serviceEstoque;
            }

        [HttpGet("ConsultarProduto/{id}")]
        public async Task<ActionResult> ConsultarProduto(int id)
        {
            try
            {
                var verificacao = await context.Produtos.FindAsync(id);
                if (verificacao == null)
                {
                    return NotFound("Produto não encontrado. Certifiquese se o ID está correto.");
                }
                var resultado = await serviceEstoque.GetConsultarProduto(id);
                return Ok(resultado);
            }
            catch (Exception x)
            {
                return BadRequest($"Erro: {x.Message}");
            }
        }
        [HttpGet("ConsultarProdutos")]
        public async Task<ActionResult> GetConsultarTodosOSProdutos()
        {
            try
            {
                var verificacao = await context.Produtos.ToListAsync();
                if(verificacao == null)
                {
                    return NotFound("Até o momento não foi adicionado nenhum produto no sistema.");
                }
                var resultado = await serviceEstoque.GetConsultarTodosOSProdutos();
                return Ok(resultado);
            }
            catch (Exception x)
            {
                return BadRequest($"Erro: {x.Message}");
            }
        }

        [HttpGet("ConsultarFornecedor")]
        public async Task<ActionResult> ConsultarFornecedor()
        {
            try
            {
                var resultado = await serviceEstoque.GetFornecedores();
                return Ok(resultado);
            }
            catch (Exception x)
            {
                return BadRequest($"Erro: {x.Message}");
            }
        }

        [HttpPost("CadastrarFornecedor")]
        public async Task<string> PostCadastrarFornecedor(Fornecedor fornecedor)
        {
            try
            {
                var resultado = await serviceEstoque.PostCadastrarFornecedor(fornecedor);
                return resultado;

            }
            catch (Exception x)
            {
                return $"Erro: {x.Message}";
            }
        }

        [HttpPost("CadastrarProduto")]
        public async Task<ActionResult> CadastrarProduto(AdicionarProduto produto)
        {
            try
            {
                var resultado = await serviceEstoque.PostCadastrarProduto(produto);
                return Ok(resultado);
            }
            catch (Exception x)
            {
                return BadRequest($"Erro: {x.Message}");
            }
        }
        [HttpPut("EditarProduto")]
        public async Task<ActionResult> PutEditarProduto(int id, double precoCusto, double precoVenda, string fornecedor)
        {
            try
            {
                var verificacao = await context.Produtos.FindAsync(id);
                if (verificacao == null)
                {
                    return NotFound("Produto não encontrado. Certifiquese se o ID está correto.");
                }

                var editar = await serviceEstoque.PutEditarProduto(id, precoCusto, precoVenda, fornecedor);
                return Ok(editar);
            }catch(Exception x)
            {
                return BadRequest($"Erro: {x.Message}");

            }
        }

        [HttpPut("ResgistraEntrada")]
        public async Task<ActionResult> PutRegistrarEntrada(int id, int quantidade)
        {
            try
            {
                var verificacao = await context.Produtos.FindAsync(id);
                if (verificacao == null)
                {
                    return NotFound("Produto não encontrado. Certifiquese se o ID está correto.");
                }

                var resultado = await serviceEstoque.PutRegistrarEntrada(id, quantidade);
                return Ok(resultado);
            }
            catch (Exception x)
            {
                return BadRequest($"Erro: {x.Message}");
            }
        }
        [HttpPut("ResgistraSaida")]
        public async Task<ActionResult> PutRegistrarSaida(int id, int quantidade)
        {
            try
            {
                var verificacao = await context.Produtos.FindAsync(id);
                var resultado = await serviceEstoque.PutRegistrarSaida(id, quantidade);


                if (verificacao == null)
                {
                    return NotFound("Produto não encontrado. Certifiquese se o ID está correto.");
                }

                var subtrair = verificacao.quantidade -= quantidade;
                if (subtrair < 0)
                {
                    return BadRequest(resultado);

                }

                return Ok(resultado);
            }
            catch (Exception x)
            {
                return BadRequest($"Erro: {x.Message}");
            }
        }

        [HttpGet("GerarRelatorioMovimentação")]
        public async Task<ActionResult> GetRelatorioMovimentação()
        {
            try
            {
                var arquivo = await serviceEstoque.GetMovimentação();
                string nomeArquivo =
                $"Relatorio_Movimentacoes_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                return File(arquivo, "text/plain", nomeArquivo);
            }
            catch (Exception x)
            {
                return BadRequest($"Erro: {x.Message}");
            }
        }
        [HttpGet("GerarRelatorioDisposição")]
        public async Task<ActionResult> GetRelatorioDisposição()
        {
            try
            {
                var arquivo = await serviceEstoque.GetRelatorioDisposição();
                string nomeArquivo =
                $"Relatorio_Disposicao_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                return File(arquivo, "text/plain", nomeArquivo);
            }
            catch (Exception x)
            {
                return BadRequest($"Erro: {x.Message}");
            }
        }

    }
}

