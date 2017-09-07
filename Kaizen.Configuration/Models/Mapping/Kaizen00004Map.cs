using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00004Map : EntityTypeConfiguration<Kaizen00004>
    {
        public Kaizen00004Map()
        {
            // Primary Key
            this.HasKey(t => t.TaskSourceID);

            // Properties
            this.Property(t => t.TaskSourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TaskSourceName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen00004");
            this.Property(t => t.TaskSourceID).HasColumnName("TaskSourceID");
            this.Property(t => t.TaskSourceName).HasColumnName("TaskSourceName");
        }
    }
}
