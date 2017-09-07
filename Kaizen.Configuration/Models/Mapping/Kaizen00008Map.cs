using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00008Map : EntityTypeConfiguration<Kaizen00008>
    {
        public Kaizen00008Map()
        {
            // Primary Key
            this.HasKey(t => new { t.FieldID, t.EXT_ScreenID });

            // Properties
            this.Property(t => t.FieldID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.EXT_ScreenID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FieldTitle)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.kaizenTableName)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Kaizen00008");
            this.Property(t => t.FieldID).HasColumnName("FieldID");
            this.Property(t => t.EXT_ScreenID).HasColumnName("EXT_ScreenID");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.FieldTitle).HasColumnName("FieldTitle");
            this.Property(t => t.kaizenTableName).HasColumnName("kaizenTableName");

            // Relationships
            this.HasRequired(t => t.Kaizen00006)
                .WithMany(t => t.Kaizen00008)
                .HasForeignKey(d => d.FieldTypeID);
            this.HasRequired(t => t.Kaizen00007)
                .WithMany(t => t.Kaizen00008)
                .HasForeignKey(d => d.EXT_ScreenID);

        }
    }
}
