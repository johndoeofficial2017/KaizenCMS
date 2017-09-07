using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class IV00100Map : EntityTypeConfiguration<IV00100>
    {
        public IV00100Map()
        {
            // Primary Key
            this.HasKey(t => t.ItemID);

            // Properties
            this.Property(t => t.ItemID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UFMGroupID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ShortDescription)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.GenericDescription)
                .HasMaxLength(50);

            this.Property(t => t.ABCID)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.UFMPurchase)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UFMSale)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(5);

            this.Property(t => t.LotNumber)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PrimaryVendor)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CountryID)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("IV00100");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.UFMGroupID).HasColumnName("UFMGroupID");
            this.Property(t => t.DecimalDigitQuantity).HasColumnName("DecimalDigitQuantity");
            this.Property(t => t.PriceMethodCode).HasColumnName("PriceMethodCode");
            this.Property(t => t.ItemTypeID).HasColumnName("ItemTypeID");
            this.Property(t => t.IsinActive).HasColumnName("IsinActive");
            this.Property(t => t.ClassID).HasColumnName("ClassID");
            this.Property(t => t.ShortDescription).HasColumnName("ShortDescription");
            this.Property(t => t.GenericDescription).HasColumnName("GenericDescription");
            this.Property(t => t.ItemDescription).HasColumnName("ItemDescription");
            this.Property(t => t.ShippingWeight).HasColumnName("ShippingWeight");
            this.Property(t => t.ABCID).HasColumnName("ABCID");
            this.Property(t => t.UFMPurchase).HasColumnName("UFMPurchase");
            this.Property(t => t.UFMSale).HasColumnName("UFMSale");
            this.Property(t => t.PriceLevelCode).HasColumnName("PriceLevelCode");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.ValuationMethodID).HasColumnName("ValuationMethodID");
            this.Property(t => t.TrackTypeID).HasColumnName("TrackTypeID");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.UnitCost).HasColumnName("UnitCost");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.PrimaryVendor).HasColumnName("PrimaryVendor");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.KaizenID).HasColumnName("KaizenID");

            // Relationships

        }
    }
}
