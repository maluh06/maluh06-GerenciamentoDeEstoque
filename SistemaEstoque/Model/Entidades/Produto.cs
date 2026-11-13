using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SistemaEstoque.Model.Entidades
{
    public class Produto
    {
        [Required]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required]
        [Display(Name = "Preço de custo")]
        public double precoCusto { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public int quantidade { get; set; }

        [Display(Name = "Fornecedor")]
        public string CnpjFornecedor { get; set; }

        [Required]
        [Display(Name = "Preço de Venda")]
        public Categorias categoria { get; set; }

        [Required]
        [Display(Name = "Preço de venda")]
        public double precoVenda { get; set; }

        [Required]
        public int estoqueMinimo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? primeiraEntrada { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ultimaEntrada { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Preço de Venda")]
        public int id { get; set; }

        public object GerarMensagem()
        {
            return new
            {
                id = id,
                nome = nome,
                precoCusto = precoCusto,
                quantidade = quantidade,
                categoria = categoria,
                precoVenda = precoVenda,
                estoqueMinimo = estoqueMinimo,
                CnpjFornecedor = CnpjFornecedor
            };
        }
    }
}
