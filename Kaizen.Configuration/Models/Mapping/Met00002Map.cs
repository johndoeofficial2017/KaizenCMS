using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00002Map : EntityTypeConfiguration<Met00002>
    {
        public Met00002Map()
        {
            // Primary Key
            this.HasKey(t => new { t.UserName, t.CalendarID });

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CalendarID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Met00002");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.CalendarID).HasColumnName("CalendarID");

            // Relationships
            this.HasRequired(t => t.Met00001)
                .WithMany(t => t.Met00002)
                .HasForeignKey(d => d.CalendarID);

        }
    }
}
