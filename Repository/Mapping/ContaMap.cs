using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mapping
{
    public class ContaMap : IEntityTypeConfiguration<Domain.Conta>
    {
        public void Configure(EntityTypeBuilder<Domain.Conta> builder)
        {
            builder.ToTable("Conta");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(150);

            builder.HasOne(x => x.Perfil).WithMany(x => x.Contas);
        }
    }
}
