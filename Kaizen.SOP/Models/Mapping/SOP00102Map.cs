using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00102Map : EntityTypeConfiguration<SOP00102>
    {
        public SOP00102Map()
        {
            // Primary Key
            this.HasKey(t => t.CUSTNMBR);

            // Properties
            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP00102");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.AccountsReceivable).HasColumnName("AccountsReceivable");
            this.Property(t => t.Sales).HasColumnName("Sales");
            this.Property(t => t.CostofGoodsSold).HasColumnName("CostofGoodsSold");
            this.Property(t => t.Inventory).HasColumnName("Inventory");
            this.Property(t => t.TermsDiscountAvailable).HasColumnName("TermsDiscountAvailable");
            this.Property(t => t.TermsDiscountTaken).HasColumnName("TermsDiscountTaken");
            this.Property(t => t.FinanceCharges).HasColumnName("FinanceCharges");
            this.Property(t => t.Writeoffs).HasColumnName("Writeoffs");
            this.Property(t => t.OverpaymentWriteoffs).HasColumnName("OverpaymentWriteoffs");
            this.Property(t => t.SalesOrderReturns).HasColumnName("SalesOrderReturns");
            this.Property(t => t.Cash).HasColumnName("Cash");

            // Relationships
            this.HasRequired(t => t.SOP00100)
                .WithOptional(t => t.SOP00102);

        }
    }
}
