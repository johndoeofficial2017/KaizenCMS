using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00001Map : EntityTypeConfiguration<Kaizen00001>
    {
        public Kaizen00001Map()
        {
            // Primary Key
            this.HasKey(t => new { t.FieldID, t.ScreenID });

            // Properties
            this.Property(t => t.FieldID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ScreenID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.kaizenTableName)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Kaizen00001");
            this.Property(t => t.FieldID).HasColumnName("FieldID");
            this.Property(t => t.ScreenID).HasColumnName("ScreenID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.kaizenTableName).HasColumnName("kaizenTableName");
            this.Property(t => t.IsDynamicTable).HasColumnName("IsDynamicTable");

            // Relationships
            this.HasRequired(t => t.Kaizen00010)
                .WithMany(t => t.Kaizen00001)
                .HasForeignKey(d => d.ScreenID);

        }
    }
}
