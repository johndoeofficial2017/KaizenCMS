using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen003Map : EntityTypeConfiguration<Kaizen003>
    {
        public Kaizen003Map()
        {
            // Primary Key
            this.HasKey(t => new { t.TaskID, t.ModuleID });

            // Properties
            this.Property(t => t.TaskID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ModuleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MenuName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ScreenPath)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Kaizen003");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.MenuName).HasColumnName("MenuName");
            this.Property(t => t.ScreenPath).HasColumnName("ScreenPath");

            // Relationships
            this.HasRequired(t => t.Kaizen000)
                .WithMany(t => t.Kaizen003)
                .HasForeignKey(d => d.ModuleID);

        }
    }
}
