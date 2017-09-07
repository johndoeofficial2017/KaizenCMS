using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00700Map : EntityTypeConfiguration<CM00700>
    {
        public CM00700Map()
        {
            // Primary Key
            this.HasKey(t => t.CaseStatusID);

            // Properties
            this.Property(t => t.CaseStatusname)
                .HasMaxLength(50);

            this.Property(t => t.RuleCondition)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("CM00700");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.CaseStatusParent).HasColumnName("CaseStatusParent");
            this.Property(t => t.StatusActionTypeID).HasColumnName("StatusActionTypeID");
            this.Property(t => t.CaseStatusname).HasColumnName("CaseStatusname");
            this.Property(t => t.RequiredDays).HasColumnName("RequiredDays");
            this.Property(t => t.IsHasChild).HasColumnName("IsHasChild");
            this.Property(t => t.IsArchive).HasColumnName("IsArchive");
            this.Property(t => t.CaseStatusTypeID).HasColumnName("CaseStatusTypeID");
            this.Property(t => t.IsPTP).HasColumnName("IsPTP");
            this.Property(t => t.IsPTPRequired).HasColumnName("IsPTPRequired");
            this.Property(t => t.IsPaymentApply).HasColumnName("IsPaymentApply");
            this.Property(t => t.IsCloseReminder).HasColumnName("IsCloseReminder");
            this.Property(t => t.IsReminder).HasColumnName("IsReminder");
            this.Property(t => t.IsTaskList).HasColumnName("IsTaskList");
            this.Property(t => t.IsScripting).HasColumnName("IsScripting");
            this.Property(t => t.RuleCondition).HasColumnName("RuleCondition");
            this.Property(t => t.StatusHTML).HasColumnName("StatusHTML");

            // Relationships
            this.HasRequired(t => t.CM00003)
                .WithMany(t => t.CM00700)
                .HasForeignKey(d => d.StatusActionTypeID);
            this.HasRequired(t => t.CM00059)
                .WithMany(t => t.CM00700)
                .HasForeignKey(d => d.CaseStatusTypeID);

        }
    }
}
