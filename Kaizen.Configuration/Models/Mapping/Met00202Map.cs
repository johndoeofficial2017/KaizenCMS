using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00202Map : EntityTypeConfiguration<Met00202>
    {
        public Met00202Map()
        {
            // Primary Key
            this.HasKey(t => t.AttendeeID);

            // Properties
            this.Property(t => t.AttendeeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Met00202");
            this.Property(t => t.AttendeeID).HasColumnName("AttendeeID");
            this.Property(t => t.AttendeeName).HasColumnName("AttendeeName");
            this.Property(t => t.MeetingID).HasColumnName("MeetingID");

            // Relationships
            this.HasRequired(t => t.Met00200)
                .WithMany(t => t.Met00202)
                .HasForeignKey(d => d.MeetingID);

        }
    }
}
