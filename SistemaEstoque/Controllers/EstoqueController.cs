using Microsoft.AspNetCore.Mvc;
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
                var resultado = await serviceEstoque.GetConsultarTodosOSProdutos();
                return Ok(resultado);
            }
            catch (Exception x)
            {
                return BadRequest($"Erro: {x.Message}");
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
        public async Task<ActionResult> PutEditarProduto(int id, double precoCusto, double precoVenda, Fornecedor fornecedor)
        {
            try
            {
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
                var resultado = await serviceEstoque.PutRegistrarSaida(id, quantidade);
                return Ok(resultado);
            }
            catch (Exception x)
            {
                return BadRequest($"Erro: {x.Message}");
            }
        }
    }
}

