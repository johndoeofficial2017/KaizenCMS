using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00002Map : EntityTypeConfiguration<Kaizen00002>
    {
        public Kaizen00002Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ScreenID, t.FieldID, t.FieldValue });

            // Properties
            this.Property(t => t.ScreenID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldValue)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Kaizen00002");
            this.Property(t => t.ScreenID).HasColumnName("ScreenID");
            this.Property(t => t.FieldID).HasColumnName("FieldID");
            this.Property(t => t.FieldValue).HasColumnName("FieldValue");

            // Relationships
            this.HasRequired(t => t.Kaizen00001)
                .WithMany(t => t.Kaizen00002)
                .HasForeignKey(d => new { d.FieldID, d.ScreenID });

        }
    }
}
