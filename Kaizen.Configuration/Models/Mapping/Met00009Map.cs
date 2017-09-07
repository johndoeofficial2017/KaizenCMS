using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00009Map : EntityTypeConfiguration<Met00009>
    {
        public Met00009Map()
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
            this.ToTable("Met00009");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.MeetingRoomID).HasColumnName("MeetingRoomID");

            // Relationships
            this.HasRequired(t => t.Met00007)
                .WithMany(t => t.Met00009)
                .HasForeignKey(d => d.MeetingRoomID);

        }
    }
}
