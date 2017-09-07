using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP20301Map : EntityTypeConfiguration<SOP20301>
    {
        public SOP20301Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.LineID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ItemID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ItemName)
                .HasMaxLength(50);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(5);

            this.Property(t => t.ItemDescription)
                .HasMaxLength(50);

            this.Property(t => t.UFMSale)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP20301");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.DecimalDigitQuantity).HasColumnName("DecimalDigitQuantity");
            this.Property(t => t.ItemDescription).HasColumnName("ItemDescription");
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
            this.HasRequired(t => t.SOP20300)
                .WithMany(t => t.SOP20301)
                .HasForeignKey(d => d.SOPNUMBE);

        }
    }
}
