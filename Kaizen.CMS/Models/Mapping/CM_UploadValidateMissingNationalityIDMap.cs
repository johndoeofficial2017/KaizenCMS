using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidateMissingNationalityIDMap : EntityTypeConfiguration<CM_UploadValidateMissingNationalityID>
    {
        public CM_UploadValidateMissingNationalityIDMap()
        {
            // Primary Key
            this.HasKey(t => t.UserName);

            // Properties
            this.Property(t => t.NationalityID)
                .HasMaxLength(300);

            this.Property(t => t.DebtorID)
                .HasMaxLength(300);

            this.Property(t => t.DebtorName)
                .HasMaxLength(300);

            this.Property(t => t.PassportNumber)
                .HasMaxLength(25);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidateMissingNationalityID");
            this.Property(t => t.NationalityID).HasColumnName("NationalityID");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.DebtorName).HasColumnName("DebtorName");
            this.Property(t => t.PassportNumber).HasColumnName("PassportNumber");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
