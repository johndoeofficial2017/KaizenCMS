using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00012Map : EntityTypeConfiguration<Met00012>
    {
        public Met00012Map()
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
            this.ToTable("Met00012");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.CalendarID).HasColumnName("CalendarID");

            // Relationships
            this.HasRequired(t => t.Met00011)
                .WithMany(t => t.Met00012)
                .HasForeignKey(d => d.CalendarID);

        }
    }
}
