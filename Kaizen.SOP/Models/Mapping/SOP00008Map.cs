using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00008Map : EntityTypeConfiguration<SOP00008>
    {
        public SOP00008Map()
        {
            // Primary Key
            this.HasKey(t => t.SalePersonTypeID);

            // Properties
            this.Property(t => t.SalePersonTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00008");
            this.Property(t => t.SalePersonTypeID).HasColumnName("SalePersonTypeID");
            this.Property(t => t.SalePersonTypeName).HasColumnName("SalePersonTypeName");
        }
    }
}
