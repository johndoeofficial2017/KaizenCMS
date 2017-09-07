using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00203Map : EntityTypeConfiguration<Met00203>
    {
        public Met00203Map()
        {
            // Primary Key
            this.HasKey(t => new { t.MeetingID, t.CaseRef });

            // Properties
            this.Property(t => t.MeetingID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Met00203");
            this.Property(t => t.MeetingID).HasColumnName("MeetingID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");

            // Relationships
            this.HasRequired(t => t.Met00200)
                .WithMany(t => t.Met00203)
                .HasForeignKey(d => d.MeetingID);

        }
    }
}
