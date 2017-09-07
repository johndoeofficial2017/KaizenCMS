using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00011Map : EntityTypeConfiguration<Met00011>
    {
        public Met00011Map()
        {
            // Primary Key
            this.HasKey(t => t.CalendarID);

            // Properties
            this.Property(t => t.CalendarName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CalendarColor)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Met00011");
            this.Property(t => t.CalendarID).HasColumnName("CalendarID");
            this.Property(t => t.CalendarName).HasColumnName("CalendarName");
            this.Property(t => t.CalendarColor).HasColumnName("CalendarColor");
        }
    }
}
