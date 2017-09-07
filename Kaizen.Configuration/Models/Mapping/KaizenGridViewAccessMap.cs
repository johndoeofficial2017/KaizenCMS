using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class KaizenGridViewAccessMap : EntityTypeConfiguration<KaizenGridViewAccess>
    {
        public KaizenGridViewAccessMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ViewID, t.UserName });

            // Properties
            this.Property(t => t.ViewID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("KaizenGridViewAccess");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");

            // Relationships
            this.HasRequired(t => t.Kaizen00011)
                .WithMany(t => t.KaizenGridViewAccesses)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
