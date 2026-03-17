using ByExternalInterfaceViewer.Models.ExternalinterfaceDBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByExternalInterfaceViewer.Services.ExternalInterfaceDB
{
    public class CassetteContentConfiguration : IEntityTypeConfiguration<CassetteContentsListModel>
    {
        public void Configure(EntityTypeBuilder<CassetteContentsListModel> entity)
        {
            entity.ToTable("ElencoContenutoCassetti");
            entity.HasKey(e => new { e.SheetGUID, e.Icon });

            entity.Property(e => e.SheetGUID)
                .HasColumnName("LamieraGUID");

            entity.Property(e => e.MaterialName)
                .HasColumnName("Materiale");

            entity.Property(e => e.MaterialDescription)
                .HasColumnName("DescrizioneMateriale");

            entity.Property(e => e.Lenght)
                .HasColumnName("Larghezza");

            entity.Property(e => e.Width)
                .HasColumnName("Profondita");

            entity.Property(e => e.Thickness)
                .HasColumnName("Spessore");

            entity.Property(e => e.Quantity)
                .HasColumnName("Quantita");

            entity.Property(e => e.SheetType)
                .HasColumnName("Tipo");

            entity.Property(e => e.CuttingPlan)
                .HasColumnName("PianoTaglio");

            entity.Property(e => e.Description)
                .HasColumnName("Descrizione");

            entity.Property(e => e.DocumentName)
                .HasColumnName("DocumentoDiTrasporto");

            entity.Property(e => e.SupplierName)
                .HasColumnName("Fornitore");

            entity.Property(e => e.CassetteID)
                .HasColumnName("Cassetto");

            entity.Property(e => e.Status)
                .HasColumnName("Stato");

            entity.Property(e => e.LocationID)
                .HasColumnName("Locazione");

            entity.Property(e => e.Icon)
                .HasColumnName("Icona");

            entity.Property(e => e.AdditionalField1)
                .HasColumnName("CampoChiave1");

            entity.Property(e => e.AdditionalField2)
                .HasColumnName("CampoChiave2");

            entity.Property(e => e.AdditionalField3)
                .HasColumnName("CampoChiave3");

            entity.Property(e => e.AdditionalField4)
                .HasColumnName("CampoChiave4");

            entity.Property(e => e.AdditionalField5)
                .HasColumnName("CampoChiave5");

            entity.Property(e => e.AdditionalField6)
                .HasColumnName("CampoLibero1");

            entity.Property(e => e.AdditionalField7)
                .HasColumnName("CampoLibero2");

            entity.Property(e => e.AdditionalField8)
                .HasColumnName("CampoLibero3");

            entity.Property(e => e.AdditionalField9)
                .HasColumnName("CampoLibero4");

            entity.Property(e => e.AdditionalField10)
                .HasColumnName("CampoLibero5");

            entity.Property(e => e.DataOperationTime)
                .HasColumnName("DataAssociazione");


        }
    }
}
