using ByExternalInterfaceViewer.Models.AWSAccessiDBModelsodels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByExternalInterfaceViewer.Services.AWSAccessiDB;

internal class LoginConfiguration : IEntityTypeConfiguration<LoginModel>
{
    public void Configure(EntityTypeBuilder<LoginModel> entity)
    {
        entity.ToTable("Utenti");
        entity.HasNoKey();

        entity.Property(e => e.Username)
            .HasColumnName("NomeUtente")
            .HasMaxLength(50);
        entity.Property(e => e.Password)
            .HasColumnName("Password")
            .HasMaxLength(50);



    }
}
