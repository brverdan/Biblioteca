using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mapping
{
    class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(250);
            builder.Property(x => x.ISBN).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Ano).IsRequired().HasMaxLength(250);

            builder.HasOne(x => x.Autor).WithMany(x => x.Livros);
        }
    }
}
