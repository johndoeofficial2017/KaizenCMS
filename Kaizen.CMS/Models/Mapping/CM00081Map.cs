using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00081Map : EntityTypeConfiguration<CM00081>
    {
        public CM00081Map()
        {
            // Primary Key
            this.HasKey(t => new { t.FieldName, t.TRXTypeID });

            // Properties
            this.Property(t => t.FieldName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.TRXTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldDisplay)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FieldEquation)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("CM00081");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldDisplay).HasColumnName("FieldDisplay");
            this.Property(t => t.IsRequired).HasColumnName("IsRequired");
            this.Property(t => t.FieldOrder).HasColumnName("FieldOrder");
            this.Property(t => t.FieldWidth).HasColumnName("FieldWidth");
            this.Property(t => t.IsEmailable).HasColumnName("IsEmailable");
            this.Property(t => t.FieldEquation).HasColumnName("FieldEquation");
            this.Property(t => t.IsPrintable).HasColumnName("IsPrintable");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Islocked).HasColumnName("Islocked");
            this.Property(t => t.Islockable).HasColumnName("Islockable");
            this.Property(t => t.IsFilterable).HasColumnName("IsFilterable");
            this.Property(t => t.IsSortable).HasColumnName("IsSortable");

            // Relationships
            this.HasRequired(t => t.CM00071)
                .WithMany(t => t.CM00081)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
