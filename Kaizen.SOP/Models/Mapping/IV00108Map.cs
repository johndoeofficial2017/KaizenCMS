using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class IV00108Map : EntityTypeConfiguration<IV00108>
    {
        public IV00108Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ItemID, t.CUSTNMBR });

            // Properties
            this.Property(t => t.ItemID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ShortDescription)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.GenericDescription)
                .HasMaxLength(50);

            this.Property(t => t.BarCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("IV00108");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.ShortDescription).HasColumnName("ShortDescription");
            this.Property(t => t.GenericDescription).HasColumnName("GenericDescription");
            this.Property(t => t.ItemDescription).HasColumnName("ItemDescription");
            this.Property(t => t.BarCode).HasColumnName("BarCode");
            this.Property(t => t.SalesAcc).HasColumnName("SalesAcc");
            this.Property(t => t.SalesReturnAcc).HasColumnName("SalesReturnAcc");
            this.Property(t => t.MarkdownAcc).HasColumnName("MarkdownAcc");
            this.Property(t => t.InventoryAcc).HasColumnName("InventoryAcc");
            this.Property(t => t.InventoryReturnAcc).HasColumnName("InventoryReturnAcc");
            this.Property(t => t.InventoryOffsetAcc).HasColumnName("InventoryOffsetAcc");
            this.Property(t => t.FreightAcc).HasColumnName("FreightAcc");
            this.Property(t => t.TradeDiscountAcc).HasColumnName("TradeDiscountAcc");
            this.Property(t => t.CostOfGoodsSold).HasColumnName("CostOfGoodsSold");
            this.Property(t => t.TaxAcc).HasColumnName("TaxAcc");

            // Relationships
          
            this.HasRequired(t => t.SOP00100)
                .WithMany(t => t.IV00108)
                .HasForeignKey(d => d.CUSTNMBR);

        }
    }
}
