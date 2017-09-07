using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen004ViewMap : EntityTypeConfiguration<Kaizen004View>
    {
        public Kaizen004ViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TaskID, t.UserName, t.ModuleID, t.MenuName, t.ScreenPath, t.MenueTypeID, t.MenueOrder, t.IsCustomPage, t.CompanyID });

            // Properties
            this.Property(t => t.TaskID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ModuleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MenuName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ScreenPath)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.MenueTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MenueOrder)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.WindowPath)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen004View");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.MenuName).HasColumnName("MenuName");
            this.Property(t => t.ScreenPath).HasColumnName("ScreenPath");
            this.Property(t => t.MenueTypeID).HasColumnName("MenueTypeID");
            this.Property(t => t.MenueOrder).HasColumnName("MenueOrder");
            this.Property(t => t.IsCustomPage).HasColumnName("IsCustomPage");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.WindowPath).HasColumnName("WindowPath");
        }
    }
}
