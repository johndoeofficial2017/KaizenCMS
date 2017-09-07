using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidateMobileNo1Map : EntityTypeConfiguration<CM_UploadValidateMobileNo1>
    {
        public CM_UploadValidateMobileNo1Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewMobileNo1)
                .HasMaxLength(100);

            this.Property(t => t.OldMobileNo1)
                .HasMaxLength(100);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidateMobileNo1");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewMobileNo1).HasColumnName("NewMobileNo1");
            this.Property(t => t.OldMobileNo1).HasColumnName("OldMobileNo1");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
