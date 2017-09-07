using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00016Map : EntityTypeConfiguration<CM00016>
    {
        public CM00016Map()
        {
            // Primary Key
            this.HasKey(t => t.Lookup01);

            // Properties
            this.Property(t => t.Lookup01Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00016");
            this.Property(t => t.Lookup01).HasColumnName("Lookup01");
            this.Property(t => t.Lookup01Name).HasColumnName("Lookup01Name");
        }
    }
}
