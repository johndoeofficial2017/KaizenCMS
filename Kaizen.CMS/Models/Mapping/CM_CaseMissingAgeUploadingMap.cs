using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_CaseMissingAgeUploadingMap : EntityTypeConfiguration<CM_CaseMissingAgeUploading>
    {
        public CM_CaseMissingAgeUploadingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AgentID, t.KaizenPublicKey });

            // Properties
            this.Property(t => t.AgentID)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM_CaseMissingAgeUploading");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.KaizenPublicKey).HasColumnName("KaizenPublicKey");
        }
    }
}
