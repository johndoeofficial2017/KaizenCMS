using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00003Map : EntityTypeConfiguration<SOP00003>
    {
        public SOP00003Map()
        {
            // Primary Key
            this.HasKey(t => t.Territory);

            // Properties
            this.Property(t => t.Territory)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TerritoryName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00003");
            this.Property(t => t.Territory).HasColumnName("Territory");
            this.Property(t => t.TerritoryName).HasColumnName("TerritoryName");
        }
    }
}
