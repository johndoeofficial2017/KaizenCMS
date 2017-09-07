using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00140Map : EntityTypeConfiguration<CM00140>
    {
        public CM00140Map()
        {
            // Primary Key
            this.HasKey(t => t.LegalID);

            // Properties
            this.Property(t => t.LegalID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.LegalName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ShortName)
                .HasMaxLength(50);

            this.Property(t => t.LegalClassID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.StatusID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00140");
            this.Property(t => t.LegalID).HasColumnName("LegalID");
            this.Property(t => t.LegalName).HasColumnName("LegalName");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.PartnerDescription).HasColumnName("PartnerDescription");
            this.Property(t => t.LegalClassID).HasColumnName("LegalClassID");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.FinanceCharge).HasColumnName("FinanceCharge");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
            this.Property(t => t.TotalCaseNumber).HasColumnName("TotalCaseNumber");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.BillAddressCode).HasColumnName("BillAddressCode");

            // Relationships
            this.HasOptional(t => t.CM00022)
                .WithMany(t => t.CM00140)
                .HasForeignKey(d => d.StatusID);
            this.HasRequired(t => t.CM00066)
                .WithMany(t => t.CM00140)
                .HasForeignKey(d => d.LegalClassID);

        }
    }
}
