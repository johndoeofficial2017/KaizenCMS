using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00102Map : EntityTypeConfiguration<Met00102>
    {
        public Met00102Map()
        {
            // Primary Key
            this.HasKey(t => t.AttendeeID);

            // Properties
            this.Property(t => t.AttendeeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Met00102");
            this.Property(t => t.AttendeeID).HasColumnName("AttendeeID");
            this.Property(t => t.AttendeeName).HasColumnName("AttendeeName");
            this.Property(t => t.MeetingID).HasColumnName("MeetingID");

            // Relationships
            this.HasRequired(t => t.Met00100)
                .WithMany(t => t.Met00102)
                .HasForeignKey(d => d.MeetingID);

        }
    }
}
