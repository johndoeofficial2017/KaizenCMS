using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00012Map : EntityTypeConfiguration<SOP00012>
    {
        public SOP00012Map()
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
            this.ToTable("SOP00012");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.PriorityName).HasColumnName("PriorityName");
        }
    }
}
