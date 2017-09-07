using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00200Map : EntityTypeConfiguration<SOP00200>
    {
        public SOP00200Map()
        {
            // Primary Key
            this.HasKey(t => t.DocNumber);

            // Properties
            this.Property(t => t.DocNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TermCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTNAME)
                .HasMaxLength(100);

            this.Property(t => t.AddressCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.EmployeeID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TerritoryID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ShippingID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DocDescription)
                .HasMaxLength(50);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ExchangeTableID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PONumber)
                .HasMaxLength(50);

            this.Property(t => t.PaymentNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00200");
            this.Property(t => t.DocNumber).HasColumnName("DocNumber");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.TermCode).HasColumnName("TermCode");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.CUSTNAME).HasColumnName("CUSTNAME");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.TerritoryID).HasColumnName("TerritoryID");
            this.Property(t => t.ShippingID).HasColumnName("ShippingID");
            this.Property(t => t.DocDescription).HasColumnName("DocDescription");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.ExchangeTableID).HasColumnName("ExchangeTableID");
            this.Property(t => t.ExchangeRateID).HasColumnName("ExchangeRateID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.DOCDATE).HasColumnName("DOCDATE");
            this.Property(t => t.PONumber).HasColumnName("PONumber");
            this.Property(t => t.AMT).HasColumnName("AMT");
            this.Property(t => t.Cost).HasColumnName("Cost");
            this.Property(t => t.TradeDiscount).HasColumnName("TradeDiscount");
            this.Property(t => t.Freight).HasColumnName("Freight");
            this.Property(t => t.Markdown).HasColumnName("Markdown");
            this.Property(t => t.Miscellaneous).HasColumnName("Miscellaneous");
            this.Property(t => t.PaymentMethod).HasColumnName("PaymentMethod");
            this.Property(t => t.PaymentAMT).HasColumnName("PaymentAMT");
            this.Property(t => t.IsDiscountPercent).HasColumnName("IsDiscountPercent");
            this.Property(t => t.TermsDiscountTaken).HasColumnName("TermsDiscountTaken");
            this.Property(t => t.PaymentNumber).HasColumnName("PaymentNumber");
            this.Property(t => t.KaizenID).HasColumnName("KaizenID");

            // Relationships
            this.HasRequired(t => t.SOP00020)
                .WithMany(t => t.SOP00200)
                .HasForeignKey(d => d.TRXTypeID);
            this.HasOptional(t => t.SOP00022)
                .WithMany(t => t.SOP00200)
                .HasForeignKey(d => d.TermCode);
            this.HasOptional(t => t.SOP00023)
                .WithMany(t => t.SOP00200)
                .HasForeignKey(d => d.TerritoryID);
            this.HasRequired(t => t.SOP00100)
                .WithMany(t => t.SOP00200)
                .HasForeignKey(d => d.CUSTNMBR);
            this.HasOptional(t => t.SOP00201)
                .WithMany(t => t.SOP00200)
                .HasForeignKey(d => d.BatchID);

        }
    }
}
