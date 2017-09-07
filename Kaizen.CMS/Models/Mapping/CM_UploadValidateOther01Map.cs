using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidateOther01Map : EntityTypeConfiguration<CM_UploadValidateOther01>
    {
        public CM_UploadValidateOther01Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewOther01)
                .HasMaxLength(100);

            this.Property(t => t.OldOther01)
                .HasMaxLength(100);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidateOther01");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewOther01).HasColumnName("NewOther01");
            this.Property(t => t.OldOther01).HasColumnName("OldOther01");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
