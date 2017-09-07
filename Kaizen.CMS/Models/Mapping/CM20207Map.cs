using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM20207Map : EntityTypeConfiguration<CM20207>
    {
        public CM20207Map()
        {
            // Primary Key
            this.HasKey(t => t.PaymentID);

            // Properties
            this.Property(t => t.PaymentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.YearCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PeriodName)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.CheckbookCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CheckbookName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TrxDescription)
                .HasMaxLength(500);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PostingBy)
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

            this.Property(t => t.VoidBy)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM20207");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.YearCode).HasColumnName("YearCode");
            this.Property(t => t.PERIODID).HasColumnName("PERIODID");
            this.Property(t => t.PeriodName).HasColumnName("PeriodName");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.CheckbookCode).HasColumnName("CheckbookCode");
            this.Property(t => t.CheckbookName).HasColumnName("CheckbookName");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.PaymentAmount).HasColumnName("PaymentAmount");
            this.Property(t => t.PaymentAmountF).HasColumnName("PaymentAmountF");
            this.Property(t => t.PaymentDate).HasColumnName("PaymentDate");
            this.Property(t => t.TrxDescription).HasColumnName("TrxDescription");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.PostingBy).HasColumnName("PostingBy");
            this.Property(t => t.PostingDate).HasColumnName("PostingDate");
            this.Property(t => t.PaymentNumber).HasColumnName("PaymentNumber");
            this.Property(t => t.BankCode).HasColumnName("BankCode");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankAccount).HasColumnName("BankAccount");
            this.Property(t => t.BankCheckDate).HasColumnName("BankCheckDate");
            this.Property(t => t.IsApproved).HasColumnName("IsApproved");
            this.Property(t => t.VoidBy).HasColumnName("VoidBy");
            this.Property(t => t.VoidDate).HasColumnName("VoidDate");
            this.Property(t => t.VoidSystemDate).HasColumnName("VoidSystemDate");
        }
    }
}
