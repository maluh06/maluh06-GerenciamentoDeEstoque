using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Model.Entidades;

namespace SistemaEstoque.Model.ApplicationDbContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Movimentacoes> Movimentacoes { get; set; }
    }
}