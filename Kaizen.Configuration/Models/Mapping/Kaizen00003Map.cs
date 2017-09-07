using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00003Map : EntityTypeConfiguration<Kaizen00003>
    {
        public Kaizen00003Map()
        {
            // Primary Key
            this.HasKey(t => t.ExtraFieldID);

            // Properties
            this.Property(t => t.ExtraFieldID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.FieldValue)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.FieldName)
                .HasMaxLength(50);

            this.Property(t => t.FieldTitle)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.kaizenTableName)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Kaizen00003");
            this.Property(t => t.ExtraFieldID).HasColumnName("ExtraFieldID");
            this.Property(t => t.ScreenID).HasColumnName("ScreenID");
            this.Property(t => t.FieldID).HasColumnName("FieldID");
            this.Property(t => t.FieldValue).HasColumnName("FieldValue");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.FieldTitle).HasColumnName("FieldTitle");
            this.Property(t => t.kaizenTableName).HasColumnName("kaizenTableName");

            // Relationships
            this.HasRequired(t => t.Kaizen00002)
                .WithMany(t => t.Kaizen00003)
                .HasForeignKey(d => new { d.ScreenID, d.FieldID, d.FieldValue });
            this.HasRequired(t => t.Kaizen00006)
                .WithMany(t => t.Kaizen00003)
                .HasForeignKey(d => d.FieldTypeID);

        }
    }
}
