using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidateOccupationMap : EntityTypeConfiguration<CM_UploadValidateOccupation>
    {
        public CM_UploadValidateOccupationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewOccupation)
                .HasMaxLength(50);

            this.Property(t => t.OldOccupation)
                .HasMaxLength(50);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidateOccupation");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewOccupation).HasColumnName("NewOccupation");
            this.Property(t => t.OldOccupation).HasColumnName("OldOccupation");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
