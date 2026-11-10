using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEstoque.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelaMovimentacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movimentacoes",
                columns: table => new
                {
                    dataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idProduto = table.Column<int>(type: "int", nullable: false),
                    quantTotal = table.Column<int>(type: "int", nullable: false),
                    UnidadeMovimentada = table.Column<int>(type: "int", nullable: false),
                    tipoMovimentacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacoes", x => x.dataEntrada);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimentacoes");
        }
    }
}
