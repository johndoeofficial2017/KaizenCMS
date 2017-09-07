using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Ext00005Map : EntityTypeConfiguration<Ext00005>
    {
        public Ext00005Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DataBaseSourceID, t.ComTableName, t.UserName });

            // Properties
            this.Property(t => t.DataBaseSourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ComTableName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Ext00005");
            this.Property(t => t.DataBaseSourceID).HasColumnName("DataBaseSourceID");
            this.Property(t => t.ComTableName).HasColumnName("ComTableName");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");

            // Relationships
            this.HasRequired(t => t.Ext00002)
                .WithMany(t => t.Ext00005)
                .HasForeignKey(d => new { d.DataBaseSourceID, d.ComTableName });

        }
    }
}
