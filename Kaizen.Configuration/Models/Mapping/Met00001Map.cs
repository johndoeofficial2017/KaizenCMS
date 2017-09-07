using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00001Map : EntityTypeConfiguration<Met00001>
    {
        public Met00001Map()
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

            this.Property(t => t.UserName)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Met00001");
            this.Property(t => t.CalendarID).HasColumnName("CalendarID");
            this.Property(t => t.CalendarName).HasColumnName("CalendarName");
            this.Property(t => t.CalendarColor).HasColumnName("CalendarColor");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
