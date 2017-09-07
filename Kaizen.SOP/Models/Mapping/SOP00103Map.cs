using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00103Map : EntityTypeConfiguration<SOP00103>
    {
        public SOP00103Map()
        {
            // Primary Key
            this.HasKey(t => t.CUSTNMBR);

            // Properties
            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP00103");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.FreightType).HasColumnName("FreightType");
            this.Property(t => t.FreightAmt).HasColumnName("FreightAmt");
            this.Property(t => t.TradeDiscountType).HasColumnName("TradeDiscountType");
            this.Property(t => t.TradeDiscount).HasColumnName("TradeDiscount");
            this.Property(t => t.FinanceChargeType).HasColumnName("FinanceChargeType");
            this.Property(t => t.FinanceChargeAmt).HasColumnName("FinanceChargeAmt");
            this.Property(t => t.MinimumPaymentType).HasColumnName("MinimumPaymentType");
            this.Property(t => t.MinimumPaymentAMT).HasColumnName("MinimumPaymentAMT");
            this.Property(t => t.CreditLimitType).HasColumnName("CreditLimitType");
            this.Property(t => t.CreditLimitAMT).HasColumnName("CreditLimitAMT");
            this.Property(t => t.MaximumWriteoffType).HasColumnName("MaximumWriteoffType");
            this.Property(t => t.MaximumWriteoffAMT).HasColumnName("MaximumWriteoffAMT");

            // Relationships
            this.HasRequired(t => t.SOP00100)
                .WithOptional(t => t.SOP00103);

        }
    }
}
