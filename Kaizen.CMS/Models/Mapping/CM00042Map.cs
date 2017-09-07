using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00042Map : EntityTypeConfiguration<CM00042>
    {
        public CM00042Map()
        {
            // Primary Key
            this.HasKey(t => t.RuleID);

            // Properties
            this.Property(t => t.RuleName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00042");
            this.Property(t => t.RuleID).HasColumnName("RuleID");
            this.Property(t => t.TableRuleID).HasColumnName("TableRuleID");
            this.Property(t => t.RuleName).HasColumnName("RuleName");
            this.Property(t => t.IsStopNext).HasColumnName("IsStopNext");
            this.Property(t => t.RuleOrder).HasColumnName("RuleOrder");

            // Relationships
            this.HasRequired(t => t.CM00041)
                .WithMany(t => t.CM00042)
                .HasForeignKey(d => d.TableRuleID);

        }
    }
}
