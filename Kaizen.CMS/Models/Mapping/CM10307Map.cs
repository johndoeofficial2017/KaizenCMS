using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM10307Map : EntityTypeConfiguration<CM10307>
    {
        public CM10307Map()
        {
            // Primary Key
            this.HasKey(t => t.PaymentID);

            // Properties
            this.Property(t => t.PaymentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.CheckbookCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ExchangeTableID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TransDescription)
                .HasMaxLength(1000);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PaymentNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BankCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BankName)
                .HasMaxLength(50);

            this.Property(t => t.BankAccount)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM10307");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.CheckbookCode).HasColumnName("CheckbookCode");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.ExchangeTableID).HasColumnName("ExchangeTableID");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.TransDescription).HasColumnName("TransDescription");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.ReasonID).HasColumnName("ReasonID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.PaymentNumber).HasColumnName("PaymentNumber");
            this.Property(t => t.BankCode).HasColumnName("BankCode");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankAccount).HasColumnName("BankAccount");
            this.Property(t => t.BankCheckDate).HasColumnName("BankCheckDate");

            // Relationships
            this.HasRequired(t => t.CM00203)
                .WithMany(t => t.CM10307)
                .HasForeignKey(d => new { d.CaseRef, d.TRXTypeID });

        }
    }
}
