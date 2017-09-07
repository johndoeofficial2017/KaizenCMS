using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10311Map : EntityTypeConfiguration<SOP10311>
    {
        public SOP10311Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.TrxNumber)
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

            this.Property(t => t.UFMSale)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP10311");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.TrxNumber).HasColumnName("TrxNumber");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.DecimalDigitQuantity).HasColumnName("DecimalDigitQuantity");
            this.Property(t => t.InvoiceOTY).HasColumnName("InvoiceOTY");
            this.Property(t => t.UFMSale).HasColumnName("UFMSale");
            this.Property(t => t.BaseUnitSale).HasColumnName("BaseUnitSale");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");

            // Relationships
            this.HasRequired(t => t.SOP10310)
                .WithMany(t => t.SOP10311)
                .HasForeignKey(d => d.TrxNumber);

        }
    }
}
