using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00101Map : EntityTypeConfiguration<Met00101>
    {
        public Met00101Map()
        {
            // Primary Key
            this.HasKey(t => t.TableID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Met00101");
            this.Property(t => t.TableID).HasColumnName("TableID");
            this.Property(t => t.CalendarID).HasColumnName("CalendarID");
            this.Property(t => t.MeetingID).HasColumnName("MeetingID");

            // Relationships
            this.HasRequired(t => t.Met00001)
                .WithMany(t => t.Met00101)
                .HasForeignKey(d => d.CalendarID);
            this.HasRequired(t => t.Met00100)
                .WithMany(t => t.Met00101)
                .HasForeignKey(d => d.MeetingID);

        }
    }
}
