using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mapping
{
    class AutorMap : IEntityTypeConfiguration<Domain.Autor>
    {
        public void Configure(EntityTypeBuilder<Domain.Autor> builder)
        {
            builder.ToTable("Autor");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Sobrenome).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.DtAniversario).IsRequired();
        }
    }
}
