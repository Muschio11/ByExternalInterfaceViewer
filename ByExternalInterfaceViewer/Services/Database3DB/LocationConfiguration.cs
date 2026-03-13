using ByExternalInterfaceViewer.Models.Database3DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByExternalInterfaceViewer.Services.Database3DB;

public class LocationConfiguration : IEntityTypeConfiguration<LocationModel>
{
    public void Configure(EntityTypeBuilder<LocationModel> entity)
    {
        entity.ToTable("Locazioni");

        entity.Property(e => e.LocationID)
            .HasColumnName("LocazioneGUID");
            
        entity.Property(e => e.LocationType)
            .HasColumnName("TipoLocazione")
            .HasMaxLength(50);
    }
}
