using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidate01Map : EntityTypeConfiguration<CM_UploadValidate01>
    {
        public CM_UploadValidate01Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewEmployerName)
                .HasMaxLength(1000);

            this.Property(t => t.OldEmployerName)
                .HasMaxLength(1000);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidate01");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewEmployerName).HasColumnName("NewEmployerName");
            this.Property(t => t.OldEmployerName).HasColumnName("OldEmployerName");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
