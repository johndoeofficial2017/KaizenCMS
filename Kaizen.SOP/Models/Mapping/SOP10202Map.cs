using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10202Map : EntityTypeConfiguration<SOP10202>
    {
        public SOP10202Map()
        {
            // Primary Key
            this.HasKey(t => t.ItemCategoryID);

            // Properties
            this.Property(t => t.ItemCategoryName)
                .HasMaxLength(50);

            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SOPTypeSetupID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP10202");
            this.Property(t => t.ItemCategoryID).HasColumnName("ItemCategoryID");
            this.Property(t => t.ItemCategoryName).HasColumnName("ItemCategoryName");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.SOPTypeSetupID).HasColumnName("SOPTypeSetupID");
            this.Property(t => t.SOPTYPE).HasColumnName("SOPTYPE");
        }
    }
}
