using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10501Map : EntityTypeConfiguration<SOP10501>
    {
        public SOP10501Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ItemLineIndex, t.SOPNUMBE });

            // Properties
            this.Property(t => t.ItemLineIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ProjectID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ItemID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(50);

            this.Property(t => t.ItemDescription)
                .HasMaxLength(50);

            this.Property(t => t.ItemName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CustomerItemID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UFMGroupID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UFMID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SiteID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LotNumber)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.VendorID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ShippingID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LineDescription)
                .HasMaxLength(1000);

            this.Property(t => t.SalePersonID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Territory)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP10501");
            this.Property(t => t.ItemLineIndex).HasColumnName("ItemLineIndex");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.ProjectID).HasColumnName("ProjectID");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.ItemDescription).HasColumnName("ItemDescription");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.CustomerItemID).HasColumnName("CustomerItemID");
            this.Property(t => t.UFMGroupID).HasColumnName("UFMGroupID");
            this.Property(t => t.UFMID).HasColumnName("UFMID");
            this.Property(t => t.DecimalDigitQuantity).HasColumnName("DecimalDigitQuantity");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.BinTrack).HasColumnName("BinTrack");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.IsExpiryDate).HasColumnName("IsExpiryDate");
            this.Property(t => t.VendorID).HasColumnName("VendorID");
            this.Property(t => t.ShippingID).HasColumnName("ShippingID");
            this.Property(t => t.ShipDate).HasColumnName("ShipDate");
            this.Property(t => t.ReqShipDate).HasColumnName("ReqShipDate");
            this.Property(t => t.LineDescription).HasColumnName("LineDescription");
            this.Property(t => t.PriceLevelCode).HasColumnName("PriceLevelCode");
            this.Property(t => t.SalePersonID).HasColumnName("SalePersonID");
            this.Property(t => t.Territory).HasColumnName("Territory");
            this.Property(t => t.QuantityOrder).HasColumnName("QuantityOrder");
            this.Property(t => t.QuantityFOC).HasColumnName("QuantityFOC");
            this.Property(t => t.QuantityCancel).HasColumnName("QuantityCancel");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.UnitCost).HasColumnName("UnitCost");
            this.Property(t => t.Markdown).HasColumnName("Markdown");
            this.Property(t => t.Miscellaneous).HasColumnName("Miscellaneous");
            this.Property(t => t.Freight).HasColumnName("Freight");
            this.Property(t => t.TradeDiscount).HasColumnName("TradeDiscount");
            this.Property(t => t.TaxAMT).HasColumnName("TaxAMT");
            this.Property(t => t.Commission).HasColumnName("Commission");

            // Relationships
            this.HasRequired(t => t.SOP10500)
                .WithMany(t => t.SOP10501)
                .HasForeignKey(d => d.SOPNUMBE);

        }
    }
}
