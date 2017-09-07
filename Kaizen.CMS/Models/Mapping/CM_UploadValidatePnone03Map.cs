using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidatePnone03Map : EntityTypeConfiguration<CM_UploadValidatePnone03>
    {
        public CM_UploadValidatePnone03Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewPnone03)
                .HasMaxLength(300);

            this.Property(t => t.OldPnone03)
                .HasMaxLength(300);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidatePnone03");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewPnone03).HasColumnName("NewPnone03");
            this.Property(t => t.OldPnone03).HasColumnName("OldPnone03");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
