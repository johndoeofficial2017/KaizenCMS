using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10102Map : EntityTypeConfiguration<SOP10102>
    {
        public SOP10102Map()
        {
            // Primary Key
            this.HasKey(t => new { t.LOTLineCode, t.LineID });

            // Properties
            this.Property(t => t.LOTLineCode)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LineID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BarCode)
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("SOP10102");
            this.Property(t => t.LOTLineCode).HasColumnName("LOTLineCode");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.BarCode).HasColumnName("BarCode");
            this.Property(t => t.AppliedQuantity).HasColumnName("AppliedQuantity");

            // Relationships
            this.HasRequired(t => t.SOP10101)
                .WithMany(t => t.SOP10102)
                .HasForeignKey(d => d.LineID);

        }
    }
}
