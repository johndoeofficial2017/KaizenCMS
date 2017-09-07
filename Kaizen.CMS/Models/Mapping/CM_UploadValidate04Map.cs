using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidate04Map : EntityTypeConfiguration<CM_UploadValidate04>
    {
        public CM_UploadValidate04Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewAddress2)
                .HasMaxLength(300);

            this.Property(t => t.OldAddress2)
                .HasMaxLength(300);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidate04");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewAddress2).HasColumnName("NewAddress2");
            this.Property(t => t.OldAddress2).HasColumnName("OldAddress2");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
