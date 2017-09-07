using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00013Map : EntityTypeConfiguration<Met00013>
    {
        public Met00013Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CalendarID, t.RoleID });

            // Properties
            this.Property(t => t.CalendarID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Met00013");
            this.Property(t => t.CalendarID).HasColumnName("CalendarID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");

            // Relationships
            this.HasRequired(t => t.Met00011)
                .WithMany(t => t.Met00013)
                .HasForeignKey(d => d.CalendarID);

        }
    }
}
