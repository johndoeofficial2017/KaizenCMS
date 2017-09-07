using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM20200Map : EntityTypeConfiguration<CM20200>
    {
        public CM20200Map()
        {
            // Primary Key
            this.HasKey(t => t.CIFNumber);

            // Properties
            this.Property(t => t.CIFNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.CIFName)
                .HasMaxLength(50);

            this.Property(t => t.AddressName)
                .HasMaxLength(300);

            this.Property(t => t.DescriptionNote)
                .HasMaxLength(500);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM20200");
            this.Property(t => t.CIFNumber).HasColumnName("CIFNumber");
            this.Property(t => t.CIFName).HasColumnName("CIFName");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
            this.Property(t => t.DescriptionNote).HasColumnName("DescriptionNote");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
        }
    }
}
