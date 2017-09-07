using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidatePnone04Map : EntityTypeConfiguration<CM_UploadValidatePnone04>
    {
        public CM_UploadValidatePnone04Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewPnone04)
                .HasMaxLength(300);

            this.Property(t => t.OldPnone04)
                .HasMaxLength(300);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidatePnone04");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewPnone04).HasColumnName("NewPnone04");
            this.Property(t => t.OldPnone04).HasColumnName("OldPnone04");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
