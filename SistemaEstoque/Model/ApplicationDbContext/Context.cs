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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .Property(p => p.id)
                .UseIdentityColumn();
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Movimentacoes> Movimentacoes { get; set; }
    }
}