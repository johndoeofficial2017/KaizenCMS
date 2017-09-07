using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00009Map : EntityTypeConfiguration<SOP00009>
    {
        public SOP00009Map()
        {
            // Primary Key
            this.HasKey(t => t.AddressTypeCode);

            // Properties
            this.Property(t => t.AddressTypeCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.AddressTypeName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP00009");
            this.Property(t => t.AddressTypeCode).HasColumnName("AddressTypeCode");
            this.Property(t => t.AddressTypeName).HasColumnName("AddressTypeName");
        }
    }
}
