using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00100Map : EntityTypeConfiguration<Sys00100>
    {
        public Sys00100Map()
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

            this.Property(t => t.UserAsginFrom)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserAsginTO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Sys00100");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.TaskTypeID).HasColumnName("TaskTypeID");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.TaskTitle).HasColumnName("TaskTitle");
            this.Property(t => t.TaskDescription).HasColumnName("TaskDescription");
            this.Property(t => t.TaskStartDate).HasColumnName("TaskStartDate");
            this.Property(t => t.TaskEndDate).HasColumnName("TaskEndDate");
            this.Property(t => t.UserAsginFrom).HasColumnName("UserAsginFrom");
            this.Property(t => t.UserAsginTO).HasColumnName("UserAsginTO");
            this.Property(t => t.AssignDate).HasColumnName("AssignDate");
            this.Property(t => t.TaskProgress).HasColumnName("TaskProgress");

        }
    }
}
