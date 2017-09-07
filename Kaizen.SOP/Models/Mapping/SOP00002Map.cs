using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00002Map : EntityTypeConfiguration<SOP00002>
    {
        public SOP00002Map()
        {
            // Primary Key
            this.HasKey(t => t.SalesPersonID);

            // Properties
            this.Property(t => t.SalesPersonID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SalesPersonName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00002");
            this.Property(t => t.SalesPersonID).HasColumnName("SalesPersonID");
            this.Property(t => t.SalesPersonName).HasColumnName("SalesPersonName");
        }
    }
}
