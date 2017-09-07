using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00021Map : EntityTypeConfiguration<SOP00021>
    {
        public SOP00021Map()
        {
            // Primary Key
            this.HasKey(t => t.DueTypeID);

            // Properties
            this.Property(t => t.DueTypeName)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP00021");
            this.Property(t => t.DueTypeID).HasColumnName("DueTypeID");
            this.Property(t => t.DueTypeName).HasColumnName("DueTypeName");
        }
    }
}
