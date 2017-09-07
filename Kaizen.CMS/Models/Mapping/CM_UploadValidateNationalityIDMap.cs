using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidateNationalityIDMap : EntityTypeConfiguration<CM_UploadValidateNationalityID>
    {
        public CM_UploadValidateNationalityIDMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewNationalityID)
                .HasMaxLength(300);

            this.Property(t => t.OldNationalityID)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidateNationalityID");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewNationalityID).HasColumnName("NewNationalityID");
            this.Property(t => t.OldNationalityID).HasColumnName("OldNationalityID");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
