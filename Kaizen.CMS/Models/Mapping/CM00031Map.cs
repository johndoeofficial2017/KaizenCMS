using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00031Map : EntityTypeConfiguration<CM00031>
    {
        public CM00031Map()
        {
            // Primary Key
            this.HasKey(t => new { t.UserName, t.AddressCode });

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AddressCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00031");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");

            // Relationships
            this.HasRequired(t => t.CM00009)
                .WithMany(t => t.CM00031)
                .HasForeignKey(d => d.AddressCode);

        }
    }
}
