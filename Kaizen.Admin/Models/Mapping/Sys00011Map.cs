using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00011Map : EntityTypeConfiguration<Sys00011>
    {
        public Sys00011Map()
        {
            // Primary Key
            this.HasKey(t => t.GenderID);

            // Properties
            this.Property(t => t.GenderName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sys00011");
            this.Property(t => t.GenderID).HasColumnName("GenderID");
            this.Property(t => t.GenderName).HasColumnName("GenderName");
        }
    }
}
