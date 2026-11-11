using SistemaEstoque.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.Model.Entidades
{
    public class Fornecedor
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [Key]
        public string CNPJ { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public object GerarMensagem()
        {
            return new
            {
                Nome = Nome,
                CNPJ = CNPJ,
                Telefone = Telefone,
                Email = Email
            };
        }
    }
}
