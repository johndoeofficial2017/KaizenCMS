using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00034Map : EntityTypeConfiguration<CM00034>
    {
        public CM00034Map()
        {
            // Primary Key
            this.HasKey(t => t.AddressCodeType);

            // Properties
            this.Property(t => t.AddressCodeType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AddressTypeName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00034");
            this.Property(t => t.AddressCodeType).HasColumnName("AddressCodeType");
            this.Property(t => t.AddressTypeName).HasColumnName("AddressTypeName");
        }
    }
}
