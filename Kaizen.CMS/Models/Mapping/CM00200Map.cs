using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00200Map : EntityTypeConfiguration<CM00200>
    {
        public CM00200Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ContractID, t.ClientID });

            // Properties
            this.Property(t => t.ContractID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ClientName)
                .HasMaxLength(100);

            this.Property(t => t.ContractName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Abbreviation)
                .HasMaxLength(25);

            this.Property(t => t.BilltoCustomer)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CurrencyCode)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00200");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.ContractName).HasColumnName("ContractName");
            this.Property(t => t.ContractStatusID).HasColumnName("ContractStatusID");
            this.Property(t => t.PaymentBaseID).HasColumnName("PaymentBaseID");
            this.Property(t => t.BillingFrequencyID).HasColumnName("BillingFrequencyID");
            this.Property(t => t.Abbreviation).HasColumnName("Abbreviation");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.BilltoCustomer).HasColumnName("BilltoCustomer");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.LastPaymentDate).HasColumnName("LastPaymentDate");
            this.Property(t => t.TargetAmount).HasColumnName("TargetAmount");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
            this.Property(t => t.IsPrivateCase).HasColumnName("IsPrivateCase");
            this.Property(t => t.Remarks).HasColumnName("Remarks");

            // Relationships
            this.HasOptional(t => t.CM00013)
                .WithMany(t => t.CM00200)
                .HasForeignKey(d => d.BillingFrequencyID);
            this.HasOptional(t => t.CM00018)
                .WithMany(t => t.CM00200)
                .HasForeignKey(d => d.PaymentBaseID);
            this.HasRequired(t => t.CM00019)
                .WithMany(t => t.CM00200)
                .HasForeignKey(d => d.ContractStatusID);
            this.HasRequired(t => t.CM00110)
                .WithMany(t => t.CM00200)
                .HasForeignKey(d => d.ClientID);

        }
    }
}
