using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidatePassportNumberMap : EntityTypeConfiguration<CM_UploadValidatePassportNumber>
    {
        public CM_UploadValidatePassportNumberMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewPassportNumber)
                .HasMaxLength(25);

            this.Property(t => t.OldPassportNumber)
                .HasMaxLength(25);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidatePassportNumber");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewPassportNumber).HasColumnName("NewPassportNumber");
            this.Property(t => t.OldPassportNumber).HasColumnName("OldPassportNumber");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
