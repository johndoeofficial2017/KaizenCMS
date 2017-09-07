using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00500Map : EntityTypeConfiguration<SOP00500>
    {
        public SOP00500Map()
        {
            // Primary Key
            this.HasKey(t => t.TrxDocumentID);

            // Properties
            this.Property(t => t.TrxDocumentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BatchID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTNAME)
                .HasMaxLength(100);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ExchangeTableID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CheckbookCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CheckbookTrxID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TrxDescription)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SOP00500");
            this.Property(t => t.TrxDocumentID).HasColumnName("TrxDocumentID");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.CUSTNAME).HasColumnName("CUSTNAME");
            this.Property(t => t.ApplyToCustomer).HasColumnName("ApplyToCustomer");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.ExchangeTableID).HasColumnName("ExchangeTableID");
            this.Property(t => t.ExchangeRateID).HasColumnName("ExchangeRateID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.DOCDATE).HasColumnName("DOCDATE");
            this.Property(t => t.PostDate).HasColumnName("PostDate");
            this.Property(t => t.DOCAMNT).HasColumnName("DOCAMNT");
            this.Property(t => t.DOCAMNTUnAply).HasColumnName("DOCAMNTUnAply");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");
            this.Property(t => t.CheckbookCode).HasColumnName("CheckbookCode");
            this.Property(t => t.CheckbookTrxID).HasColumnName("CheckbookTrxID");
            this.Property(t => t.TrxDescription).HasColumnName("TrxDescription");

            // Relationships
            this.HasRequired(t => t.SOP00501)
                .WithMany(t => t.SOP00500)
                .HasForeignKey(d => d.BatchID);
            this.HasRequired(t => t.Sys00020)
                .WithMany(t => t.SOP00500)
                .HasForeignKey(d => d.PaymentMethodID);

        }
    }
}
