using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.CompanyID);

            // Properties
            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SegmentMark)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GLFormat)
                .HasMaxLength(50);

            this.Property(t => t.CurrencyCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ExchangeTableID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CheckbookCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SOPTypeSetupID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SiteID)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Company");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.SegmentMark).HasColumnName("SegmentMark");
            this.Property(t => t.GLFormat).HasColumnName("GLFormat");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.ExchangeTableID).HasColumnName("ExchangeTableID");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.ExchangeRateID).HasColumnName("ExchangeRateID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.YearCode).HasColumnName("YearCode");
            this.Property(t => t.CurrentDate).HasColumnName("CurrentDate");
            this.Property(t => t.CheckbookCode).HasColumnName("CheckbookCode");
            this.Property(t => t.SOPTypeSetupID).HasColumnName("SOPTypeSetupID");
            this.Property(t => t.SOPTYPE).HasColumnName("SOPTYPE");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
        }
    }
}
