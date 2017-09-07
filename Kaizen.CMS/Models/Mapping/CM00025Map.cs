using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00025Map : EntityTypeConfiguration<CM00025>
    {
        public CM00025Map()
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

            this.Property(t => t.TaskTitle)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TaskDescription)
                .IsRequired();

            this.Property(t => t.AgentID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SqlScript)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("CM00025");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.TaskTypeID).HasColumnName("TaskTypeID");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.TaskTitle).HasColumnName("TaskTitle");
            this.Property(t => t.TaskDescription).HasColumnName("TaskDescription");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.TaskProgress).HasColumnName("TaskProgress");
            this.Property(t => t.RequiredDays).HasColumnName("RequiredDays");
            this.Property(t => t.CaseStatusIDEffect).HasColumnName("CaseStatusIDEffect");
            this.Property(t => t.SqlScript).HasColumnName("SqlScript");

            // Relationships
            this.HasRequired(t => t.CM00700)
                .WithMany(t => t.CM00025)
                .HasForeignKey(d => d.CaseStatusID);

        }
    }
}
