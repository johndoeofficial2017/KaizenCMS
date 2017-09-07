using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00006Map : EntityTypeConfiguration<CM00006>
    {
        public CM00006Map()
        {
            // Primary Key
            this.HasKey(t => t.PriorityID);

            // Properties
            this.Property(t => t.PriorityID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PriorityName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00006");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.PriorityName).HasColumnName("PriorityName");
        }
    }
}
