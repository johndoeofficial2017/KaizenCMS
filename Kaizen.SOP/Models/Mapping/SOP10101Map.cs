using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10101Map : EntityTypeConfiguration<SOP10101>
    {
        public SOP10101Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
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
                .HasMaxLength(25);

            this.Property(t => t.CustomerItemID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ShipAddressTypeCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.ShipAddressName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Pnone01)
                .HasMaxLength(50);

            this.Property(t => t.Pnone02)
                .HasMaxLength(50);

            this.Property(t => t.MobileNo1)
                .HasMaxLength(50);

            this.Property(t => t.MobileNo2)
                .HasMaxLength(50);

            this.Property(t => t.POBox)
                .HasMaxLength(50);

            this.Property(t => t.Other01)
                .HasMaxLength(50);

            this.Property(t => t.Other02)
                .HasMaxLength(50);

            this.Property(t => t.Address1)
                .HasMaxLength(50);

            this.Property(t => t.Email01)
                .HasMaxLength(50);

            this.Property(t => t.Email02)
                .HasMaxLength(50);

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

            this.Property(t => t.VendorID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.VendorItemID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.VendorItemName)
                .HasMaxLength(20);

            this.Property(t => t.VendorShortDescription)
                .HasMaxLength(50);

            this.Property(t => t.CountryID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ShippingID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LineDescription)
                .HasMaxLength(1000);

            this.Property(t => t.SalePersonID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Territory)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP10101");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.ProjectID).HasColumnName("ProjectID");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.DecimalDigitQuantity).HasColumnName("DecimalDigitQuantity");
            this.Property(t => t.ItemDescription).HasColumnName("ItemDescription");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.CustomerItemID).HasColumnName("CustomerItemID");
            this.Property(t => t.ShipAddressTypeCode).HasColumnName("ShipAddressTypeCode");
            this.Property(t => t.ShipAddressName).HasColumnName("ShipAddressName");
            this.Property(t => t.Pnone01).HasColumnName("Pnone01");
            this.Property(t => t.Pnone02).HasColumnName("Pnone02");
            this.Property(t => t.MobileNo1).HasColumnName("MobileNo1");
            this.Property(t => t.MobileNo2).HasColumnName("MobileNo2");
            this.Property(t => t.POBox).HasColumnName("POBox");
            this.Property(t => t.Other01).HasColumnName("Other01");
            this.Property(t => t.Other02).HasColumnName("Other02");
            this.Property(t => t.Address1).HasColumnName("Address1");
            this.Property(t => t.Email01).HasColumnName("Email01");
            this.Property(t => t.Email02).HasColumnName("Email02");
            this.Property(t => t.UFMGroupID).HasColumnName("UFMGroupID");
            this.Property(t => t.UFMID).HasColumnName("UFMID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.BinTrack).HasColumnName("BinTrack");
            this.Property(t => t.VendorID).HasColumnName("VendorID");
            this.Property(t => t.VendorItemID).HasColumnName("VendorItemID");
            this.Property(t => t.VendorItemName).HasColumnName("VendorItemName");
            this.Property(t => t.VendorShortDescription).HasColumnName("VendorShortDescription");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.ShippingID).HasColumnName("ShippingID");
            this.Property(t => t.ShipDate).HasColumnName("ShipDate");
            this.Property(t => t.ReqShipDate).HasColumnName("ReqShipDate");
            this.Property(t => t.LineDescription).HasColumnName("LineDescription");
            this.Property(t => t.PriceLevelCode).HasColumnName("PriceLevelCode");
            this.Property(t => t.SalePersonID).HasColumnName("SalePersonID");
            this.Property(t => t.Territory).HasColumnName("Territory");
            this.Property(t => t.QuantityOrder).HasColumnName("QuantityOrder");
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
            this.HasOptional(t => t.IV00100)
                .WithMany(t => t.SOP10101)
                .HasForeignKey(d => d.ItemID);
            this.HasRequired(t => t.SOP00003)
                .WithMany(t => t.SOP10101)
                .HasForeignKey(d => d.Territory);
            this.HasOptional(t => t.SOP00024)
                .WithMany(t => t.SOP10101)
                .HasForeignKey(d => d.ShippingID);
            this.HasRequired(t => t.SOP10100)
                .WithMany(t => t.SOP10101)
                .HasForeignKey(d => d.SOPNUMBE);

        }
    }
}
