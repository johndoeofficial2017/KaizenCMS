using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00005Map : EntityTypeConfiguration<CM00005>
    {
        public CM00005Map()
        {
            // Primary Key
            this.HasKey(t => t.CalendarID);

            // Properties
            this.Property(t => t.CalendarName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00005");
            this.Property(t => t.CalendarID).HasColumnName("CalendarID");
            this.Property(t => t.CalendarName).HasColumnName("CalendarName");
            this.Property(t => t.PeriodCount).HasColumnName("PeriodCount");
        }
    }
}
