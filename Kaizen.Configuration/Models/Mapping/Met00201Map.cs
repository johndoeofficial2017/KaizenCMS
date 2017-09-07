using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00201Map : EntityTypeConfiguration<Met00201>
    {
        public Met00201Map()
        {
            // Primary Key
            this.HasKey(t => t.TableID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Met00201");
            this.Property(t => t.TableID).HasColumnName("TableID");
            this.Property(t => t.CalendarID).HasColumnName("CalendarID");
            this.Property(t => t.MeetingID).HasColumnName("MeetingID");

            // Relationships
            this.HasRequired(t => t.Met00011)
                .WithMany(t => t.Met00201)
                .HasForeignKey(d => d.CalendarID);
            this.HasRequired(t => t.Met00200)
                .WithMany(t => t.Met00201)
                .HasForeignKey(d => d.MeetingID);

        }
    }
}
