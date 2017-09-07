using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00204Map : EntityTypeConfiguration<CM00204>
    {
        public CM00204Map()
        {
            // Primary Key
            this.HasKey(t => new { t.PaymentID, t.CaseRef, t.TRXTypeID });

            // Properties
            this.Property(t => t.PaymentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.TRXTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ClientName)
                .HasMaxLength(100);

            this.Property(t => t.ContractID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ContractName)
                .HasMaxLength(50);

            this.Property(t => t.ProductName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00204");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.ContractName).HasColumnName("ContractName");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.Amount).HasColumnName("Amount");

            // Relationships
            this.HasRequired(t => t.CM00203)
                .WithMany(t => t.CM00204)
                .HasForeignKey(d => new { d.CaseRef, d.TRXTypeID });
            this.HasRequired(t => t.CM00207)
                .WithMany(t => t.CM00204)
                .HasForeignKey(d => d.PaymentID);

        }
    }
}
