using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00000Map : EntityTypeConfiguration<SOP00000>
    {
        public SOP00000Map()
        {
            // Primary Key
            this.HasKey(t => t.SOPTYPE);

            // Properties
            this.Property(t => t.SOPTYPENAME)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00000");
            this.Property(t => t.SOPTYPE).HasColumnName("SOPTYPE");
            this.Property(t => t.SOPTYPENAME).HasColumnName("SOPTYPENAME");
        }
    }
}
