using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen002Map : EntityTypeConfiguration<Kaizen002>
    {
        public Kaizen002Map()
        {
            // Primary Key
            this.HasKey(t => new { t.TaskID, t.ModuleID, t.MenueTypeID });

            // Properties
            this.Property(t => t.TaskID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ModuleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MenueTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MenuName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ScreenPath)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.WindowPath)
                .HasMaxLength(50);

            this.Property(t => t.WindowParm)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen002");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.MenueTypeID).HasColumnName("MenueTypeID");
            this.Property(t => t.MenuName).HasColumnName("MenuName");
            this.Property(t => t.ScreenPath).HasColumnName("ScreenPath");
            this.Property(t => t.IsCustomPage).HasColumnName("IsCustomPage");
            this.Property(t => t.MenueOrder).HasColumnName("MenueOrder");
            this.Property(t => t.WindowPath).HasColumnName("WindowPath");
            this.Property(t => t.WindowParm).HasColumnName("WindowParm");

            // Relationships
            this.HasRequired(t => t.Kaizen000)
                .WithMany(t => t.Kaizen002)
                .HasForeignKey(d => d.ModuleID);
            this.HasRequired(t => t.Kaizen001)
                .WithMany(t => t.Kaizen002)
                .HasForeignKey(d => d.MenueTypeID);

        }
    }
}
