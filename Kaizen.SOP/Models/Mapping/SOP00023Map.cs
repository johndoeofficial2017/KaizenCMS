using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00023Map : EntityTypeConfiguration<SOP00023>
    {
        public SOP00023Map()
        {
            // Primary Key
            this.HasKey(t => t.TerritoryID);

            // Properties
            this.Property(t => t.TerritoryID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TerritoryName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00023");
            this.Property(t => t.TerritoryID).HasColumnName("TerritoryID");
            this.Property(t => t.TerritoryName).HasColumnName("TerritoryName");
        }
    }
}
