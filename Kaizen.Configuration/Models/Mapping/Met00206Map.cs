using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00206Map : EntityTypeConfiguration<Met00206>
    {
        public Met00206Map()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.MeetingRoomID });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MeetingRoomID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Met00206");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.MeetingRoomID).HasColumnName("MeetingRoomID");

            // Relationships
            this.HasRequired(t => t.Met00204)
                .WithMany(t => t.Met00206)
                .HasForeignKey(d => d.MeetingRoomID);

        }
    }
}
