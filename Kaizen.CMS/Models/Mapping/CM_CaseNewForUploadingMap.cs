using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_CaseNewForUploadingMap : EntityTypeConfiguration<CM_CaseNewForUploading>
    {
        public CM_CaseNewForUploadingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CaseRef, t.DebtorID, t.CaseAccountNo, t.AgentID, t.UserName });

            // Properties
            this.Property(t => t.CaseRef)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DebtorID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CaseAddess)
                .HasMaxLength(500);

            this.Property(t => t.CaseAccountNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.InvoiceNumber)
                .HasMaxLength(50);

            this.Property(t => t.AgentID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AssignDate)
                .HasMaxLength(50);

            this.Property(t => t.LastPaymentDate)
                .HasMaxLength(50);

            this.Property(t => t.LastPaymentBy)
                .HasMaxLength(50);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_CaseNewForUploading");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.BucketID).HasColumnName("BucketID");
            this.Property(t => t.CaseAddess).HasColumnName("CaseAddess");
            this.Property(t => t.CaseAccountNo).HasColumnName("CaseAccountNo");
            this.Property(t => t.InvoiceNumber).HasColumnName("InvoiceNumber");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.OSAmount).HasColumnName("OSAmount");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.PrincipleAmount).HasColumnName("PrincipleAmount");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.AssignDate).HasColumnName("AssignDate");
            this.Property(t => t.LastPaymentDate).HasColumnName("LastPaymentDate");
            this.Property(t => t.LastCallactedAMT).HasColumnName("LastCallactedAMT");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
            this.Property(t => t.LastPaymentBy).HasColumnName("LastPaymentBy");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
