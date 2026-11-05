using SistemaEstoque.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SistemaEstoque.Model.Entidades
{
    public class Produto
    {
        [Required]
        public string nome { get; set; }

        [Required]
        public double precoCusto { get; set; }

        [Required]
        public int quantidade { get; set; }

        [Required]
        public Fornecedor fornecedor { get; set; }

        [Required]
        public Categorias categoria { get; set; }

        [Required]
        public double precoVenda { get; set; }

        [Required]
        public int estoqueMinimo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime primeiraEntrada { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ultimaEntrada { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
    }
}
