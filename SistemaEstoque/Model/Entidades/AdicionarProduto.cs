using SistemaEstoque.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.Model.Entidades
{
    public class AdicionarProduto : Produto
    {
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Display(Name = "Preço de custo")]
        public double precoCusto { get; set; }

        [Display(Name = "Quantidade")]
        public int quantidade { get; set; }

        [Display(Name = "Fornecedor")]
        public Fornecedor fornecedor { get; set; }

        [Display(Name = "Categoria")]
        public Categorias categoria { get; set; }

        [Display(Name = "Preço de venda")]
        public double precoVenda { get; set; }

        [Display(Name = "Estoque Mínimo do produto")]
        [Required]
        public int estoqueMinimo { get; set; }
    }
}
