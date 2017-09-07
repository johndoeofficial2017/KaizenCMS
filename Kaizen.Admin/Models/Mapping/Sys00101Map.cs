using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00101Map : EntityTypeConfiguration<Sys00101>
    {
        public Sys00101Map()
        {
            // Primary Key
            this.HasKey(t => t.ActionID);

            // Properties
            this.Property(t => t.TaskDescription)
                .IsRequired();

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PhotoExtension)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.UserAsginTO)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Sys00101");
            this.Property(t => t.ActionID).HasColumnName("ActionID");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.TaskProgress).HasColumnName("TaskProgress");
            this.Property(t => t.TaskDescription).HasColumnName("TaskDescription");
            this.Property(t => t.TaskDate).HasColumnName("TaskDate");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.UserAsginTO).HasColumnName("UserAsginTO");

            // Relationships
            this.HasRequired(t => t.Sys00100)
                .WithMany(t => t.Sys00101)
                .HasForeignKey(d => d.TaskID);

        }
    }
}
