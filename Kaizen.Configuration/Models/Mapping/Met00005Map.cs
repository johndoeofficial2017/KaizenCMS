using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00005Map : EntityTypeConfiguration<Met00005>
    {
        public Met00005Map()
        {
            // Primary Key
            this.HasKey(t => t.MeetingRepeatTypeID);

            // Properties
            this.Property(t => t.MeetingRepeatTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MeetingRepeatTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Met00005");
            this.Property(t => t.MeetingRepeatTypeID).HasColumnName("MeetingRepeatTypeID");
            this.Property(t => t.MeetingRepeatTypeName).HasColumnName("MeetingRepeatTypeName");
        }
    }
}
