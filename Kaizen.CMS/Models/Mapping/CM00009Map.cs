using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00009Map : EntityTypeConfiguration<CM00009>
    {
        public CM00009Map()
        {
            // Primary Key
            this.HasKey(t => t.AddressCode);

            // Properties
            this.Property(t => t.AddressCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AddressName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00009");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
        }
    }
}
