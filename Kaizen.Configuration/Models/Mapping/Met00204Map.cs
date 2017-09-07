using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00204Map : EntityTypeConfiguration<Met00204>
    {
        public Met00204Map()
        {
            // Primary Key
            this.HasKey(t => t.MeetingRoomID);

            // Properties
            this.Property(t => t.MeetingRoomName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Met00204");
            this.Property(t => t.MeetingRoomID).HasColumnName("MeetingRoomID");
            this.Property(t => t.MeetingRoomName).HasColumnName("MeetingRoomName");
        }
    }
}
