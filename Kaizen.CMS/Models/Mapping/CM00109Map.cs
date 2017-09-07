using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00109Map : EntityTypeConfiguration<CM00109>
    {
        public CM00109Map()
        {
            // Primary Key
            this.HasKey(t => new { t.TargetID, t.AgentID });

            // Properties
            this.Property(t => t.TargetID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SQLCondition)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("CM00109");
            this.Property(t => t.TargetID).HasColumnName("TargetID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.TargetTypeID).HasColumnName("TargetTypeID");
            this.Property(t => t.IsPercentTarget).HasColumnName("IsPercentTarget");
            this.Property(t => t.SQLCondition).HasColumnName("SQLCondition");

            // Relationships
            this.HasRequired(t => t.CM00105)
                .WithMany(t => t.CM00109)
                .HasForeignKey(d => d.AgentID);
            this.HasRequired(t => t.CM00108)
                .WithMany(t => t.CM00109)
                .HasForeignKey(d => d.TargetID);

        }
    }
}
