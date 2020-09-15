using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Context.Repository
{
    public class BibliotecaContext : DbContext
    {
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AutorMap());
            modelBuilder.ApplyConfiguration(new LivroMap());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class BloggingContextFactory : IDesignTimeDbContextFactory<BibliotecaContext>
    {
        public BibliotecaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BibliotecaDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new BibliotecaContext(optionsBuilder.Options);
        }
    }
}
