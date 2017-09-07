using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00008Map : EntityTypeConfiguration<Sys00008>
    {
        public Sys00008Map()
        {
            // Primary Key
            this.HasKey(t => t.ReligionID);

            // Properties
            this.Property(t => t.ReligionID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ReligionName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sys00008");
            this.Property(t => t.ReligionID).HasColumnName("ReligionID");
            this.Property(t => t.ReligionName).HasColumnName("ReligionName");
        }
    }
}
