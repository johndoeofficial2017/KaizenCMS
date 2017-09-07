using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM10100Map : EntityTypeConfiguration<CM10100>
    {
        public CM10100Map()
        {
            // Primary Key
            this.HasKey(t => t.BatchID);

            // Properties
            this.Property(t => t.BatchID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ContractID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ClientName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContractName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreditorID)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.CreditorName)
                .HasMaxLength(50);

            this.Property(t => t.YearCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PeriodName)
                .HasMaxLength(25);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UploadedFileName)
                .HasMaxLength(50);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM10100");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.ContractName).HasColumnName("ContractName");
            this.Property(t => t.CreditorID).HasColumnName("CreditorID");
            this.Property(t => t.CreditorName).HasColumnName("CreditorName");
            this.Property(t => t.YearCode).HasColumnName("YearCode");
            this.Property(t => t.PERIODID).HasColumnName("PERIODID");
            this.Property(t => t.PeriodName).HasColumnName("PeriodName");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.ClosingDate).HasColumnName("ClosingDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UploadedFileName).HasColumnName("UploadedFileName");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");

            // Relationships
            this.HasRequired(t => t.CM00200)
                .WithMany(t => t.CM10100)
                .HasForeignKey(d => new { d.ContractID, d.ClientID });

        }
    }
}
