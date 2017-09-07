using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00601ViewMap : EntityTypeConfiguration<SOP00601View>
    {
        public SOP00601ViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ItemID, t.SiteID });

            // Properties
            this.Property(t => t.ItemID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.SiteID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP00601View");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.QuantityValue).HasColumnName("QuantityValue");
            this.Property(t => t.UnitCost).HasColumnName("UnitCost");
        }
    }
}
