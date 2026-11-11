using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEstoque.Migrations
{
    /// <inheritdoc />
    public partial class MudançanaexibiçãodetodososdadosdoFornecedornatabelaProdutosrestrigiapenasparaexigirseuCNPJ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Fornecedores_fornecedorCNPJ",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_fornecedorCNPJ",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "fornecedorCNPJ",
                table: "Produtos");

            migrationBuilder.AddColumn<string>(
                name: "CnpjFornecedor",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CnpjFornecedor",
                table: "Produtos");

            migrationBuilder.AddColumn<string>(
                name: "fornecedorCNPJ",
                table: "Produtos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_fornecedorCNPJ",
                table: "Produtos",
                column: "fornecedorCNPJ");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Fornecedores_fornecedorCNPJ",
                table: "Produtos",
                column: "fornecedorCNPJ",
                principalTable: "Fornecedores",
                principalColumn: "CNPJ",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
