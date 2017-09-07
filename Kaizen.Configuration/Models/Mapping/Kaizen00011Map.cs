using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00011Map : EntityTypeConfiguration<Kaizen00011>
    {
        public Kaizen00011Map()
        {
            // Primary Key
            this.HasKey(t => t.ViewID);

            // Properties
            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ViewName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ViewDescription)
                .HasMaxLength(100);

            this.Property(t => t.ControllerSource)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ViewWhereCondition)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("Kaizen00011");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.ScreenID).HasColumnName("ScreenID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.ViewName).HasColumnName("ViewName");
            this.Property(t => t.ViewDescription).HasColumnName("ViewDescription");
            this.Property(t => t.IsEmailable).HasColumnName("IsEmailable");
            this.Property(t => t.IsPrintable).HasColumnName("IsPrintable");
            this.Property(t => t.IsPivotTable).HasColumnName("IsPivotTable");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.IsCustomView).HasColumnName("IsCustomView");
            this.Property(t => t.ControllerSource).HasColumnName("ControllerSource");
            this.Property(t => t.ViewWhereCondition).HasColumnName("ViewWhereCondition");
            this.Property(t => t.ViewSortCondition).HasColumnName("ViewSortCondition");
            this.Property(t => t.ViewStateData).HasColumnName("ViewStateData");

            // Relationships
            this.HasMany(t => t.Kaizen00030)
                .WithMany(t => t.Kaizen00011)
                .Map(m =>
                    {
                        m.ToTable("Kaizen005");
                        m.MapLeftKey("ViewID");
                        m.MapRightKey("RoleID");
                    });

            this.HasRequired(t => t.Company)
                .WithMany(t => t.Kaizen00011)
                .HasForeignKey(d => d.CompanyID);
            this.HasRequired(t => t.Kaizen00010)
                .WithMany(t => t.Kaizen00011)
                .HasForeignKey(d => d.ScreenID);

        }
    }
}
