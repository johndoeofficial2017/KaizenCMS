using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00003Map : EntityTypeConfiguration<Met00003>
    {
        public Met00003Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CalendarID, t.RoleID });

            // Properties
            this.Property(t => t.CalendarID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Met00003");
            this.Property(t => t.CalendarID).HasColumnName("CalendarID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");

            // Relationships
            this.HasRequired(t => t.Met00001)
                .WithMany(t => t.Met00003)
                .HasForeignKey(d => d.CalendarID);

        }
    }
}
