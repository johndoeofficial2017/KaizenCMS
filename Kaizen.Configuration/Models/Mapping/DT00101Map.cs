using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class DT00101Map : EntityTypeConfiguration<DT00101>
    {
        public DT00101Map()
        {
            // Primary Key
            this.HasKey(t => new { t.SourceID, t.FieldName });

            // Properties
            this.Property(t => t.SourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.FieldDisplay)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DT00101");
            this.Property(t => t.SourceID).HasColumnName("SourceID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldDisplay).HasColumnName("FieldDisplay");
            this.Property(t => t.IsColumn01).HasColumnName("IsColumn01");
            this.Property(t => t.IsRequired).HasColumnName("IsRequired");
            this.Property(t => t.FieldOrder).HasColumnName("FieldOrder");
            this.Property(t => t.FieldWidth).HasColumnName("FieldWidth");

            // Relationships
            this.HasRequired(t => t.DT00100)
                .WithMany(t => t.DT00101)
                .HasForeignKey(d => d.SourceID);
            this.HasRequired(t => t.Kaizen00006)
                .WithMany(t => t.DT00101)
                .HasForeignKey(d => d.FieldTypeID);

        }
    }
}
