using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace EFCore20
{
    public class Context : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        public Context()
        { }

        public Context(DbContextOptions<Context> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=EFCore20DB;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.ApplyConfiguration(new ProdutoMap());
            construtorDeModelos.ApplyConfiguration(new CategoriaMap());

            // Global query filter
            construtorDeModelos.Entity<Produto>().HasQueryFilter(p => p.Ativo);
        }
    }
}
