using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Ext00002Map : EntityTypeConfiguration<Ext00002>
    {
        public Ext00002Map()
        {
            // Primary Key
            this.HasKey(t => new { t.DataBaseSourceID, t.ComTableName });

            // Properties
            this.Property(t => t.DataBaseSourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ComTableName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.SourceDisplay)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Ext00002");
            this.Property(t => t.DataBaseSourceID).HasColumnName("DataBaseSourceID");
            this.Property(t => t.ComTableName).HasColumnName("ComTableName");
            this.Property(t => t.SourceDisplay).HasColumnName("SourceDisplay");
            this.Property(t => t.IsAddNew).HasColumnName("IsAddNew");
            this.Property(t => t.IsformOnly).HasColumnName("IsformOnly");
            this.Property(t => t.IsView).HasColumnName("IsView");
            this.Property(t => t.IsGraph).HasColumnName("IsGraph");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.MenueTypeID).HasColumnName("MenueTypeID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasRequired(t => t.Ext00001)
                .WithMany(t => t.Ext00002)
                .HasForeignKey(d => d.DataBaseSourceID);

        }
    }
}
