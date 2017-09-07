using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00017Map : EntityTypeConfiguration<CM00017>
    {
        public CM00017Map()
        {
            // Primary Key
            this.HasKey(t => t.Lookup02);

            // Properties
            this.Property(t => t.Lookup02Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00017");
            this.Property(t => t.Lookup02).HasColumnName("Lookup02");
            this.Property(t => t.Lookup02Name).HasColumnName("Lookup02Name");
        }
    }
}
