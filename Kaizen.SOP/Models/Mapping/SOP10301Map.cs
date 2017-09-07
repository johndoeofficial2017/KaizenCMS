using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10301Map : EntityTypeConfiguration<SOP10301>
    {
        public SOP10301Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ORGSOPNUMBE)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ItemID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ItemName)
                .HasMaxLength(50);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(5);

            this.Property(t => t.UFMSale)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP10301");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.ORGSOPNUMBE).HasColumnName("ORGSOPNUMBE");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.DecimalDigitQuantity).HasColumnName("DecimalDigitQuantity");
            this.Property(t => t.InvoiceOTY).HasColumnName("InvoiceOTY");
            this.Property(t => t.UFMSale).HasColumnName("UFMSale");
            this.Property(t => t.BaseUnitSale).HasColumnName("BaseUnitSale");
            this.Property(t => t.UnitCost).HasColumnName("UnitCost");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.Commission).HasColumnName("Commission");
            this.Property(t => t.Freight).HasColumnName("Freight");
            this.Property(t => t.TradeDiscount).HasColumnName("TradeDiscount");
            this.Property(t => t.TaxAMT).HasColumnName("TaxAMT");
            this.Property(t => t.Miscellaneous).HasColumnName("Miscellaneous");

            // Relationships
            this.HasRequired(t => t.SOP10300)
                .WithMany(t => t.SOP10301)
                .HasForeignKey(d => d.SOPNUMBE);

        }
    }
}
