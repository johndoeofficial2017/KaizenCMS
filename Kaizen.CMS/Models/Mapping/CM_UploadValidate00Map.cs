using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_UploadValidate00Map : EntityTypeConfiguration<CM_UploadValidate00>
    {
        public CM_UploadValidate00Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.ConditionVariable, t.AcctionStep });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.NewValue)
                .HasMaxLength(50);

            this.Property(t => t.OldValue)
                .HasMaxLength(200);

            this.Property(t => t.UserName)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ConditionVariable)
                .IsRequired()
                .HasMaxLength(22);

            this.Property(t => t.AcctionStep)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM_UploadValidate00");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.NewValue).HasColumnName("NewValue");
            this.Property(t => t.OldValue).HasColumnName("OldValue");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.ConditionVariable).HasColumnName("ConditionVariable");
            this.Property(t => t.AcctionStep).HasColumnName("AcctionStep");
        }
    }
}
