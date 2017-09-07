using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidate03Map : EntityTypeConfiguration<CM_UploadValidate03>
    {
        public CM_UploadValidate03Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewAddress1)
                .HasMaxLength(300);

            this.Property(t => t.OldAddress1)
                .HasMaxLength(300);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidate03");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewAddress1).HasColumnName("NewAddress1");
            this.Property(t => t.OldAddress1).HasColumnName("OldAddress1");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
