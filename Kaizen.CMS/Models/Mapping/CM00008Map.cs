using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00008Map : EntityTypeConfiguration<CM00008>
    {
        public CM00008Map()
        {
            // Primary Key
            this.HasKey(t => t.AddressCodeType);

            // Properties
            this.Property(t => t.AddressTypeName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00008");
            this.Property(t => t.AddressCodeType).HasColumnName("AddressCodeType");
            this.Property(t => t.AddressTypeName).HasColumnName("AddressTypeName");
        }
    }
}
