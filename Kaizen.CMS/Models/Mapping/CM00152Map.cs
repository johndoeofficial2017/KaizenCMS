using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00152Map : EntityTypeConfiguration<CM00152>
    {
        public CM00152Map()
        {
            // Primary Key
            this.HasKey(t => t.UserName);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BatchID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TrxTypeName)
                .HasMaxLength(50);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ClientName)
                .HasMaxLength(100);

            this.Property(t => t.NationaltyClient)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.ContractID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ContractName)
                .HasMaxLength(50);

            this.Property(t => t.CurrencyCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseStatusname)
                .HasMaxLength(50);

            this.Property(t => t.CaseStatusChildName)
                .HasMaxLength(50);

            this.Property(t => t.CaseStatusComment)
                .HasMaxLength(1000);

            this.Property(t => t.AddressCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.FileName)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("CM00152");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.TrxTypeName).HasColumnName("TrxTypeName");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.NationaltyClient).HasColumnName("NationaltyClient");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.ContractName).HasColumnName("ContractName");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.CaseStatusChild).HasColumnName("CaseStatusChild");
            this.Property(t => t.CaseStatusname).HasColumnName("CaseStatusname");
            this.Property(t => t.CaseStatusChildName).HasColumnName("CaseStatusChildName");
            this.Property(t => t.CaseStatusComment).HasColumnName("CaseStatusComment");
            this.Property(t => t.ReminderDateTime).HasColumnName("ReminderDateTime");
            this.Property(t => t.PTPAMT).HasColumnName("PTPAMT");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.ClosingDate).HasColumnName("ClosingDate");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.FileName).HasColumnName("FileName");
        }
    }
}
