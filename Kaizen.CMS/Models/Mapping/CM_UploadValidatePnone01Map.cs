using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidatePnone01Map : EntityTypeConfiguration<CM_UploadValidatePnone01>
    {
        public CM_UploadValidatePnone01Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewPnone01)
                .HasMaxLength(300);

            this.Property(t => t.OldPnone01)
                .HasMaxLength(300);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidatePnone01");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewPnone01).HasColumnName("NewPnone01");
            this.Property(t => t.OldPnone01).HasColumnName("OldPnone01");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
