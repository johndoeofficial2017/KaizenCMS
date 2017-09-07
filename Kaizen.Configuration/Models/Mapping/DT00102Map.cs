using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class DT00102Map : EntityTypeConfiguration<DT00102>
    {
        public DT00102Map()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.SourceID });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DT00102");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.SourceID).HasColumnName("SourceID");

            // Relationships
            this.HasRequired(t => t.DT00100)
                .WithMany(t => t.DT00102)
                .HasForeignKey(d => d.SourceID);

        }
    }
}
