using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_CaseOldForUploadingMap : EntityTypeConfiguration<CM_CaseOldForUploading>
    {
        public CM_CaseOldForUploadingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CaseRef, t.DebtorID, t.DebtorName, t.ClaimAmount, t.AgentID, t.ClientID });

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.DebtorName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CaseAccountNo)
                .HasMaxLength(50);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseStatusname)
                .HasMaxLength(50);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("CM_CaseOldForUploading");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.DebtorName).HasColumnName("DebtorName");
            this.Property(t => t.CaseAccountNo).HasColumnName("CaseAccountNo");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.CaseStatusname).HasColumnName("CaseStatusname");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.KaizenPublicKey).HasColumnName("KaizenPublicKey");
        }
    }
}
