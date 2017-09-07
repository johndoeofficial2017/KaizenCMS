using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Ext00003Map : EntityTypeConfiguration<Ext00003>
    {
        public Ext00003Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DataBaseSourceID, t.ComTableName, t.FieldName });

            // Properties
            this.Property(t => t.DataBaseSourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ComTableName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.FieldName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.FieldDisplay)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Ext00003");
            this.Property(t => t.DataBaseSourceID).HasColumnName("DataBaseSourceID");
            this.Property(t => t.ComTableName).HasColumnName("ComTableName");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldDisplay).HasColumnName("FieldDisplay");
            this.Property(t => t.IsColumn01).HasColumnName("IsColumn01");
            this.Property(t => t.IsRequired).HasColumnName("IsRequired");
            this.Property(t => t.FieldOrder).HasColumnName("FieldOrder");
            this.Property(t => t.FieldWidth).HasColumnName("FieldWidth");

            // Relationships
            this.HasRequired(t => t.Ext00002)
                .WithMany(t => t.Ext00003)
                .HasForeignKey(d => new { d.DataBaseSourceID, d.ComTableName });
            this.HasRequired(t => t.Kaizen00006)
                .WithMany(t => t.Ext00003)
                .HasForeignKey(d => d.FieldTypeID);

        }
    }
}
