using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10510Map : EntityTypeConfiguration<SOP10510>
    {
        public SOP10510Map()
        {
            // Primary Key
            this.HasKey(t => t.BatchID);

            // Properties
            this.Property(t => t.BatchID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BatchName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP10510");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.BatchName).HasColumnName("BatchName");
        }
    }
}
