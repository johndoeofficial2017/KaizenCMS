using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00070Map : EntityTypeConfiguration<CM00070>
    {
        public CM00070Map()
        {
            // Primary Key
            this.HasKey(t => new { t.TRXTypeID, t.FieldName });

            // Properties
            this.Property(t => t.TRXTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.FieldDisplay)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FieldEquation)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("CM00070");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
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
            this.HasRequired(t => t.CM00028)
                .WithMany(t => t.CM00070)
                .HasForeignKey(d => d.FieldTypeID);
            this.HasRequired(t => t.CM00029)
                .WithMany(t => t.CM00070)
                .HasForeignKey(d => d.TRXTypeID);

        }
    }
}
