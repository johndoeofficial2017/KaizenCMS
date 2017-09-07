using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00205Map : EntityTypeConfiguration<Met00205>
    {
        public Met00205Map()
        {
            // Primary Key
            this.HasKey(t => new { t.UserName, t.MeetingRoomID });

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.MeetingRoomID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Met00205");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.MeetingRoomID).HasColumnName("MeetingRoomID");

            // Relationships
            this.HasRequired(t => t.Met00204)
                .WithMany(t => t.Met00205)
                .HasForeignKey(d => d.MeetingRoomID);

        }
    }
}
