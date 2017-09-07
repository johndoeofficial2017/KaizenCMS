using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00005Map : EntityTypeConfiguration<Kaizen00005>
    {
        public Kaizen00005Map()
        {
            // Primary Key
            this.HasKey(t => t.TaskID);

            // Properties
            this.Property(t => t.AsginFrom)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AsginTo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TaskName)
                .HasMaxLength(15);

            this.Property(t => t.TaskTitle)
                .HasMaxLength(20);

            this.Property(t => t.TaskTransactionID)
                .HasMaxLength(50);

            this.Property(t => t.TaskDescription)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen00005");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.AsginFrom).HasColumnName("AsginFrom");
            this.Property(t => t.AsginTo).HasColumnName("AsginTo");
            this.Property(t => t.TaskName).HasColumnName("TaskName");
            this.Property(t => t.TaskTitle).HasColumnName("TaskTitle");
            this.Property(t => t.TaskSourceID).HasColumnName("TaskSourceID");
            this.Property(t => t.TaskTransactionID).HasColumnName("TaskTransactionID");
            this.Property(t => t.TaskStartDate).HasColumnName("TaskStartDate");
            this.Property(t => t.TaskDueDate).HasColumnName("TaskDueDate");
            this.Property(t => t.TaskDescription).HasColumnName("TaskDescription");
            this.Property(t => t.TaskComplated).HasColumnName("TaskComplated");

            // Relationships
            this.HasRequired(t => t.Kaizen00004)
                .WithMany(t => t.Kaizen00005)
                .HasForeignKey(d => d.TaskSourceID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Kaizen00005)
                .HasForeignKey(d => d.AsginTo);

        }
    }
}
