using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00013Map : EntityTypeConfiguration<Sys00013>
    {
        public Sys00013Map()
        {
            // Primary Key
            this.HasKey(t => t.CountryID);

            // Properties
            this.Property(t => t.CountryID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CountryName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sys00013");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CountryName).HasColumnName("CountryName");
        }
    }
}
