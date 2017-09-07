using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Ext00004Map : EntityTypeConfiguration<Ext00004>
    {
        public Ext00004Map()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.DataBaseSourceID, t.ComTableName });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DataBaseSourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ComTableName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Ext00004");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.DataBaseSourceID).HasColumnName("DataBaseSourceID");
            this.Property(t => t.ComTableName).HasColumnName("ComTableName");

            // Relationships
            this.HasRequired(t => t.Ext00002)
                .WithMany(t => t.Ext00004)
                .HasForeignKey(d => new { d.DataBaseSourceID, d.ComTableName });

        }
    }
}
