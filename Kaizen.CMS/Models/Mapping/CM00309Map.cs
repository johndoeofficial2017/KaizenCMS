using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00309Map : EntityTypeConfiguration<CM00309>
    {
        public CM00309Map()
        {
            // Primary Key
            this.HasKey(t => t.ReasonID);

            // Properties
            this.Property(t => t.ReasonName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00309");
            this.Property(t => t.ReasonID).HasColumnName("ReasonID");
            this.Property(t => t.ReasonName).HasColumnName("ReasonName");
        }
    }
}
