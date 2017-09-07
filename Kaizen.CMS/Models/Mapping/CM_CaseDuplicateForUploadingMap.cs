using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_CaseDuplicateForUploadingMap : EntityTypeConfiguration<CM_CaseDuplicateForUploading>
    {
        public CM_CaseDuplicateForUploadingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CaseRef, t.DebtorID, t.CaseAccountNo });

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

            // Table & Column Mappings
            this.ToTable("CM_CaseDuplicateForUploading");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.CaseAddess).HasColumnName("CaseAddess");
            this.Property(t => t.CaseAccountNo).HasColumnName("CaseAccountNo");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
        }
    }
}
