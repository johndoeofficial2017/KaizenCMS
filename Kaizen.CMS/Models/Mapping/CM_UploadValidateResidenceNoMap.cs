using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidateResidenceNoMap : EntityTypeConfiguration<CM_UploadValidateResidenceNo>
    {
        public CM_UploadValidateResidenceNoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewResidenceNo)
                .HasMaxLength(25);

            this.Property(t => t.OldResidenceNo)
                .HasMaxLength(25);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidateResidenceNo");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewResidenceNo).HasColumnName("NewResidenceNo");
            this.Property(t => t.OldResidenceNo).HasColumnName("OldResidenceNo");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
