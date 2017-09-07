using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00055Map : EntityTypeConfiguration<CM00055>
    {
        public CM00055Map()
        {
            // Primary Key
            this.HasKey(t => t.ProductID);

            // Properties
            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00055");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
        }
    }
}
