using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00012Map : EntityTypeConfiguration<Sys00012>
    {
        public Sys00012Map()
        {
            // Primary Key
            this.HasKey(t => t.AddressCode);

            // Properties
            this.Property(t => t.AddressName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Sys00012");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
        }
    }
}
