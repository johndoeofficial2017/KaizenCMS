using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00013Map : EntityTypeConfiguration<Kaizen00013>
    {
        public Kaizen00013Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ScreenID, t.FieldName });

            // Properties
            this.Property(t => t.ScreenID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FieldTitle)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.kaizenTableName)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Kaizen00013");
            this.Property(t => t.ScreenID).HasColumnName("ScreenID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldTitle).HasColumnName("FieldTitle");
            this.Property(t => t.kaizenTableName).HasColumnName("kaizenTableName");

            // Relationships
            this.HasRequired(t => t.Kaizen00006)
                .WithMany(t => t.Kaizen00013)
                .HasForeignKey(d => d.FieldTypeID);
            this.HasRequired(t => t.Kaizen00010)
                .WithMany(t => t.Kaizen00013)
                .HasForeignKey(d => d.ScreenID);

        }
    }
}
