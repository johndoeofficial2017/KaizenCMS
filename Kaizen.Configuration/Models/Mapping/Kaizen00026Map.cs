using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00026Map : EntityTypeConfiguration<Kaizen00026>
    {
        public Kaizen00026Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ViewID, t.field });

            // Properties
            this.Property(t => t.ViewID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.field)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.format)
                .HasMaxLength(50);

            this.Property(t => t.template)
                .HasMaxLength(50);

            this.Property(t => t.FieldEquation)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Kaizen00026");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.field).HasColumnName("field");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.width).HasColumnName("width");
            this.Property(t => t.ColumnOrder).HasColumnName("ColumnOrder");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.locked).HasColumnName("locked");
            this.Property(t => t.lockable).HasColumnName("lockable");
            this.Property(t => t.filterable).HasColumnName("filterable");
            this.Property(t => t.sortable).HasColumnName("sortable");
            this.Property(t => t.hidden).HasColumnName("hidden");
            this.Property(t => t.menu).HasColumnName("menu");
            this.Property(t => t.format).HasColumnName("format");
            this.Property(t => t.template).HasColumnName("template");
            this.Property(t => t.FieldEquation).HasColumnName("FieldEquation");
            this.Property(t => t.IsPrintable).HasColumnName("IsPrintable");
            this.Property(t => t.IsEmailable).HasColumnName("IsEmailable");

            // Relationships
            this.HasRequired(t => t.Kaizen00011)
                .WithMany(t => t.Kaizen00026)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
