using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10505Map : EntityTypeConfiguration<SOP10505>
    {
        public SOP10505Map()
        {
            // Primary Key
            this.HasKey(t => new { t.BinID, t.SubBinID });

            // Properties
            this.Property(t => t.BinID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SubBinID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("SOP10505");
            this.Property(t => t.BinID).HasColumnName("BinID");
            this.Property(t => t.SubBinID).HasColumnName("SubBinID");
            this.Property(t => t.ItemQuantity).HasColumnName("ItemQuantity");

            // Relationships
            this.HasRequired(t => t.SOP10504)
                .WithMany(t => t.SOP10505)
                .HasForeignKey(d => d.BinID);

        }
    }
}
