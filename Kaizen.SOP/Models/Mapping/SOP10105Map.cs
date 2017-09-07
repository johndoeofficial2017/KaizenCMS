using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10105Map : EntityTypeConfiguration<SOP10105>
    {
        public SOP10105Map()
        {
            // Primary Key
            this.HasKey(t => new { t.LineID, t.BinID, t.SubBinID });

            // Properties
            this.Property(t => t.LineID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BinID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SubBinID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("SOP10105");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.BinID).HasColumnName("BinID");
            this.Property(t => t.SubBinID).HasColumnName("SubBinID");
            this.Property(t => t.ItemQuantity).HasColumnName("ItemQuantity");

            // Relationships
            this.HasRequired(t => t.SOP10104)
                .WithMany(t => t.SOP10105)
                .HasForeignKey(d => new { d.LineID, d.BinID });

        }
    }
}
