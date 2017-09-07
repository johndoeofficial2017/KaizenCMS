using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00008Map : EntityTypeConfiguration<Met00008>
    {
        public Met00008Map()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.MeetingRoomID });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MeetingRoomID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Met00008");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.MeetingRoomID).HasColumnName("MeetingRoomID");

            // Relationships
            this.HasRequired(t => t.Met00007)
                .WithMany(t => t.Met00008)
                .HasForeignKey(d => d.MeetingRoomID);

        }
    }
}
