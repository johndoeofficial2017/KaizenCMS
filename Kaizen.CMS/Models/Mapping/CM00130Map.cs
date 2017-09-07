using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00130Map : EntityTypeConfiguration<CM00130>
    {
        public CM00130Map()
        {
            // Primary Key
            this.HasKey(t => t.PartnerID);

            // Properties
            this.Property(t => t.PartnerID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.PartnerName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ShortName)
                .HasMaxLength(50);

            this.Property(t => t.PartnerClassID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.StatusID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(50);

            this.Property(t => t.ParentClientID)
                .IsFixedLength()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("CM00130");
            this.Property(t => t.PartnerID).HasColumnName("PartnerID");
            this.Property(t => t.PartnerName).HasColumnName("PartnerName");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.PartnerDescription).HasColumnName("PartnerDescription");
            this.Property(t => t.PartnerClassID).HasColumnName("PartnerClassID");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.FinanceCharge).HasColumnName("FinanceCharge");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
            this.Property(t => t.TotalCaseNumber).HasColumnName("TotalCaseNumber");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.ParentClientID).HasColumnName("ParentClientID");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.BillAddressCode).HasColumnName("BillAddressCode");

            // Relationships
            this.HasRequired(t => t.CM00002)
                .WithMany(t => t.CM00130)
                .HasForeignKey(d => d.PartnerClassID);
            this.HasOptional(t => t.CM00022)
                .WithMany(t => t.CM00130)
                .HasForeignKey(d => d.StatusID);

        }
    }
}
