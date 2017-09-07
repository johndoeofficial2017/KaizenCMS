using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00214Map : EntityTypeConfiguration<CM00214>
    {
        public CM00214Map()
        {
            // Primary Key
            this.HasKey(t => t.ActionID);

            // Properties
            this.Property(t => t.TaskDescription)
                .IsRequired();

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CM00214");
            this.Property(t => t.ActionID).HasColumnName("ActionID");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.TaskProgress).HasColumnName("TaskProgress");
            this.Property(t => t.TaskDescription).HasColumnName("TaskDescription");
            this.Property(t => t.TaskDate).HasColumnName("TaskDate");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.IsCanceled).HasColumnName("IsCanceled");

            // Relationships
            this.HasRequired(t => t.CM00213)
                .WithMany(t => t.CM00214)
                .HasForeignKey(d => d.TaskID);

        }
    }
}
