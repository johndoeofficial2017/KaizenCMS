using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00012Map : EntityTypeConfiguration<CM00012>
    {
        public CM00012Map()
        {
            // Primary Key
            this.HasKey(t => t.PriorityID);

            // Properties
            this.Property(t => t.PriorityName)
                .HasMaxLength(50);

            this.Property(t => t.PriorityColor)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00012");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.PriorityName).HasColumnName("PriorityName");
            this.Property(t => t.PriorityColor).HasColumnName("PriorityColor");
        }
    }
}
