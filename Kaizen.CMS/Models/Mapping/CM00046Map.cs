using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00046Map : EntityTypeConfiguration<CM00046>
    {
        public CM00046Map()
        {
            // Primary Key
            this.HasKey(t => t.TaskID);

            // Properties
            this.Property(t => t.TaskID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ActionID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.TaskTitle)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.TaskDescription)
                .IsRequired();

            this.Property(t => t.AssignTO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00046");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.ActionID).HasColumnName("ActionID");
            this.Property(t => t.TaskTitle).HasColumnName("TaskTitle");
            this.Property(t => t.TaskDescription).HasColumnName("TaskDescription");
            this.Property(t => t.TaskStartDate).HasColumnName("TaskStartDate");
            this.Property(t => t.TaskEndDate).HasColumnName("TaskEndDate");
            this.Property(t => t.AssignTO).HasColumnName("AssignTO");
            this.Property(t => t.AssignDate).HasColumnName("AssignDate");

            // Relationships
            this.HasRequired(t => t.CM00044)
                .WithMany(t => t.CM00046)
                .HasForeignKey(d => d.ActionID);

        }
    }
}
