using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00022Map : EntityTypeConfiguration<SOP00022>
    {
        public SOP00022Map()
        {
            // Primary Key
            this.HasKey(t => t.TermCode);

            // Properties
            this.Property(t => t.TermCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP00022");
            this.Property(t => t.TermCode).HasColumnName("TermCode");
            this.Property(t => t.DueType).HasColumnName("DueType");
            this.Property(t => t.Due).HasColumnName("Due");
            this.Property(t => t.DiscountType).HasColumnName("DiscountType");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.IsPercent).HasColumnName("IsPercent");
            this.Property(t => t.DiscountValue).HasColumnName("DiscountValue");
            this.Property(t => t.FinanceChrgesIsPercent).HasColumnName("FinanceChrgesIsPercent");
            this.Property(t => t.FinanceChargeValue).HasColumnName("FinanceChargeValue");
            this.Property(t => t.ApplyTo).HasColumnName("ApplyTo");
            this.Property(t => t.ApplyDiscount).HasColumnName("ApplyDiscount");
            this.Property(t => t.ApplyFreight).HasColumnName("ApplyFreight");
            this.Property(t => t.ApplyMiscellaneous).HasColumnName("ApplyMiscellaneous");

            // Relationships
            this.HasRequired(t => t.SOP00021)
                .WithMany(t => t.SOP00022)
                .HasForeignKey(d => d.DueType);

        }
    }
}
