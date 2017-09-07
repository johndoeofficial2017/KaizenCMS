using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00213Map : EntityTypeConfiguration<CM00213>
    {
        public CM00213Map()
        {
            // Primary Key
            this.HasKey(t => t.TaskID);

            // Properties
            this.Property(t => t.TaskTypeID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PriorityID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.TaskTitle)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TaskDescription)
                .IsRequired();

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00213");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.TaskIDSource).HasColumnName("TaskIDSource");
            this.Property(t => t.TaskTypeID).HasColumnName("TaskTypeID");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.TaskTitle).HasColumnName("TaskTitle");
            this.Property(t => t.TaskDescription).HasColumnName("TaskDescription");
            this.Property(t => t.TaskStartDate).HasColumnName("TaskStartDate");
            this.Property(t => t.TaskEndDate).HasColumnName("TaskEndDate");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.AssignDate).HasColumnName("AssignDate");
            this.Property(t => t.TaskProgress).HasColumnName("TaskProgress");

            // Relationships
            this.HasRequired(t => t.CM00004)
                .WithMany(t => t.CM00213)
                .HasForeignKey(d => d.TaskTypeID);
            this.HasRequired(t => t.CM00006)
                .WithMany(t => t.CM00213)
                .HasForeignKey(d => d.PriorityID);
            this.HasRequired(t => t.CM00203)
                .WithMany(t => t.CM00213)
                .HasForeignKey(d => new { d.CaseRef, d.TRXTypeID });

        }
    }
}
