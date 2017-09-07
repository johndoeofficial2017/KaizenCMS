using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidateEmail01Map : EntityTypeConfiguration<CM_UploadValidateEmail01>
    {
        public CM_UploadValidateEmail01Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewEmail01)
                .HasMaxLength(100);

            this.Property(t => t.OldEmail01)
                .HasMaxLength(100);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidateEmail01");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewEmail01).HasColumnName("NewEmail01");
            this.Property(t => t.OldEmail01).HasColumnName("OldEmail01");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
