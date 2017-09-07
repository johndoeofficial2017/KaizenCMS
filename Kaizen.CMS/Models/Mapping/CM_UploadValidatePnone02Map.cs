using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidatePnone02Map : EntityTypeConfiguration<CM_UploadValidatePnone02>
    {
        public CM_UploadValidatePnone02Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewPnone02)
                .HasMaxLength(300);

            this.Property(t => t.OldPnone02)
                .HasMaxLength(300);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidatePnone02");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewPnone02).HasColumnName("NewPnone02");
            this.Property(t => t.OldPnone02).HasColumnName("OldPnone02");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
