using SistemaEstoque.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.Model.Entidades
{
    public class Movimentacoes
    {

        [Required]
        [Display(Name = "Nome do produto")]
        public string nomeProduto { get; set; }

        [Required]
        [Display(Name = "Id do produto")]
        public int idProduto { get; set; }

        [Required]
        [Display(Name = "Quantidade total do produto em estoque")]
        public int quantTotal{ get; set; }

        [Required]
        [Display(Name = "Quantidade de unidades movimentadas")]
        public int UnidadeMovimentada { get; set; }

        [Required]
        [Display(Name = "Tipo de movimentação")]
        public TipoMovimentacao tipoMovimentacao { get; set; }

        [Key]
        [Display(Name = "Horário da movimentação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime dataEntrada { get; set; }

        public string GerarMensagem()
        {
            return "Id do produto: " + idProduto +
                 "\nNome do produto: " + nomeProduto +
                 "\nQuantidade total em estoque: " + quantTotal +
                 "\nTipo de movimentação: " + tipoMovimentacao + " -> " +
                  UnidadeMovimentada + " unidades movimentadas " +
                 "\nHorário da movimentação: " + dataEntrada +
                 "\n";
        }
    }
}
