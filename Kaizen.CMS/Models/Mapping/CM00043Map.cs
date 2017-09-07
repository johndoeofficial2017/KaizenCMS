using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00043Map : EntityTypeConfiguration<CM00043>
    {
        public CM00043Map()
        {
            // Primary Key
            this.HasKey(t => t.RuleConditionID);

            // Properties
            this.Property(t => t.RuleConditionName)
                .HasMaxLength(50);

            this.Property(t => t.FieldID)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.FieldValues)
                .HasMaxLength(500);

            this.Property(t => t.ParentFieldID)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00043");
            this.Property(t => t.RuleConditionID).HasColumnName("RuleConditionID");
            this.Property(t => t.RuleConditionName).HasColumnName("RuleConditionName");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.FieldID).HasColumnName("FieldID");
            this.Property(t => t.OperatorID).HasColumnName("OperatorID");
            this.Property(t => t.FieldValues).HasColumnName("FieldValues");
            this.Property(t => t.ParentFieldID).HasColumnName("ParentFieldID");
            this.Property(t => t.ParentOperatorID).HasColumnName("ParentOperatorID");

            // Relationships
            this.HasOptional(t => t.CM00001)
                .WithMany(t => t.CM00043)
                .HasForeignKey(d => d.OperatorID);

        }
    }
}
