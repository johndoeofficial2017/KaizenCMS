using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidate02Map : EntityTypeConfiguration<CM_UploadValidate02>
    {
        public CM_UploadValidate02Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidate02");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewBirthDate).HasColumnName("NewBirthDate");
            this.Property(t => t.OldBirthDate).HasColumnName("OldBirthDate");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
