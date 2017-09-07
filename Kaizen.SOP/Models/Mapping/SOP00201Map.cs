using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00201Map : EntityTypeConfiguration<SOP00201>
    {
        public SOP00201Map()
        {
            // Primary Key
            this.HasKey(t => t.BatchID);

            // Properties
            this.Property(t => t.BatchName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00201");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.BatchName).HasColumnName("BatchName");
        }
    }
}
