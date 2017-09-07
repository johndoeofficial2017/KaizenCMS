using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00016Map : EntityTypeConfiguration<Sys00016>
    {
        public Sys00016Map()
        {
            // Primary Key
            this.HasKey(t => t.AddressType);

            // Properties
            this.Property(t => t.AddressTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sys00016");
            this.Property(t => t.AddressType).HasColumnName("AddressType");
            this.Property(t => t.AddressTypeName).HasColumnName("AddressTypeName");
        }
    }
}
