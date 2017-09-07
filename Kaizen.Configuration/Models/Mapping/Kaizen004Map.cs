using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen004Map : EntityTypeConfiguration<Kaizen004>
    {
        public Kaizen004Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CompanyID, t.RoleID, t.TaskID, t.ModuleID, t.MenueTypeID });

            // Properties
            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

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
                .HasMaxLength(100);

            this.Property(t => t.WindowPath)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen004");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.MenueTypeID).HasColumnName("MenueTypeID");
            this.Property(t => t.MenuName).HasColumnName("MenuName");
            this.Property(t => t.ScreenPath).HasColumnName("ScreenPath");
            this.Property(t => t.IsCustomPage).HasColumnName("IsCustomPage");
            this.Property(t => t.MenueOrder).HasColumnName("MenueOrder");
            this.Property(t => t.WindowPath).HasColumnName("WindowPath");

            // Relationships
            this.HasRequired(t => t.Kaizen00030)
                .WithMany(t => t.Kaizen004)
                .HasForeignKey(d => d.RoleID);
            this.HasRequired(t => t.Kaizen00101)
                .WithMany(t => t.Kaizen004)
                .HasForeignKey(d => new { d.CompanyID, d.ModuleID });
            this.HasRequired(t => t.Kaizen002)
                .WithMany(t => t.Kaizen004)
                .HasForeignKey(d => new { d.TaskID, d.ModuleID, d.MenueTypeID });

        }
    }
}
