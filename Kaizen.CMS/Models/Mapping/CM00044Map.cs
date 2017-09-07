using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00044Map : EntityTypeConfiguration<CM00044>
    {
        public CM00044Map()
        {
            // Primary Key
            this.HasKey(t => t.ActionID);

            // Properties
            this.Property(t => t.ActionID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ActionName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FieldName)
                .HasMaxLength(50);

            this.Property(t => t.FieldValue)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00044");
            this.Property(t => t.ActionID).HasColumnName("ActionID");
            this.Property(t => t.FunctionTypeID).HasColumnName("FunctionTypeID");
            this.Property(t => t.RuleID).HasColumnName("RuleID");
            this.Property(t => t.ActionName).HasColumnName("ActionName");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.FieldValue).HasColumnName("FieldValue");

            // Relationships
            this.HasRequired(t => t.CM00042)
                .WithMany(t => t.CM00044)
                .HasForeignKey(d => d.RuleID);

        }
    }
}
