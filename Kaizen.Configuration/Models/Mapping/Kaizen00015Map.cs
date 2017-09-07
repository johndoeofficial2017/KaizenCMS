using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00015Map : EntityTypeConfiguration<Kaizen00015>
    {
        public Kaizen00015Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CompanyID, t.TRXTypeID, t.ScreenID, t.FieldName });

            // Properties
            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TRXTypeID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ScreenID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FieldDisplay)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FieldValue)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Kaizen00015");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.ScreenID).HasColumnName("ScreenID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.FieldDisplay).HasColumnName("FieldDisplay");
            this.Property(t => t.FieldValue).HasColumnName("FieldValue");
            this.Property(t => t.required).HasColumnName("required");
            this.Property(t => t.FieldOrder).HasColumnName("FieldOrder");

            // Relationships
            this.HasRequired(t => t.Kaizen00016)
                .WithMany(t => t.Kaizen00015)
                .HasForeignKey(d => new { d.TRXTypeID, d.CompanyID });

        }
    }
}
