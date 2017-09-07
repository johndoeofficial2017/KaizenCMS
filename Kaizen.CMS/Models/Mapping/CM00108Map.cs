using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00108Map : EntityTypeConfiguration<CM00108>
    {
        public CM00108Map()
        {
            // Primary Key
            this.HasKey(t => t.TargetID);

            // Properties
            this.Property(t => t.TargetID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TargetName)
                .HasMaxLength(50);

            this.Property(t => t.SQLCondition)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("CM00108");
            this.Property(t => t.TargetID).HasColumnName("TargetID");
            this.Property(t => t.TargetTypeID).HasColumnName("TargetTypeID");
            this.Property(t => t.IsPercentTarget).HasColumnName("IsPercentTarget");
            this.Property(t => t.TargetName).HasColumnName("TargetName");
            this.Property(t => t.SQLCondition).HasColumnName("SQLCondition");

            // Relationships
            this.HasOptional(t => t.CM00058)
                .WithMany(t => t.CM00108)
                .HasForeignKey(d => d.TargetTypeID);

        }
    }
}
