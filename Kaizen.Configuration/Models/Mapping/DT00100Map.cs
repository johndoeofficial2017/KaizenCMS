using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class DT00100Map : EntityTypeConfiguration<DT00100>
    {
        public DT00100Map()
        {
            // Primary Key
            this.HasKey(t => t.SourceID);

            // Properties
            this.Property(t => t.SourceName)
                .HasMaxLength(50);

            this.Property(t => t.SourceDisplay)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SourceSql)
                .HasMaxLength(4000);

            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SourceHTML)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("DT00100");
            this.Property(t => t.SourceID).HasColumnName("SourceID");
            this.Property(t => t.SourceName).HasColumnName("SourceName");
            this.Property(t => t.SourceDisplay).HasColumnName("SourceDisplay");
            this.Property(t => t.SourceSql).HasColumnName("SourceSql");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.MenueTypeID).HasColumnName("MenueTypeID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.IsGraph).HasColumnName("IsGraph");
            this.Property(t => t.IsDataTable).HasColumnName("IsDataTable");
            this.Property(t => t.IsAddNew).HasColumnName("IsAddNew");
            this.Property(t => t.IsEdit).HasColumnName("IsEdit");
            this.Property(t => t.SourceHTML).HasColumnName("SourceHTML");
            this.Property(t => t.ViewType).HasColumnName("ViewType");
            this.Property(t => t.ViewSource).HasColumnName("ViewSource");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.DT00100)
                .HasForeignKey(d => d.CompanyID);
            this.HasOptional(t => t.DT00001)
                .WithMany(t => t.DT00100)
                .HasForeignKey(d => d.ViewType);
            this.HasRequired(t => t.Kaizen000)
                .WithMany(t => t.DT00100)
                .HasForeignKey(d => d.ModuleID);
            this.HasRequired(t => t.Kaizen001)
                .WithMany(t => t.DT00100)
                .HasForeignKey(d => d.MenueTypeID);

        }
    }
}
