using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00014Map : EntityTypeConfiguration<Kaizen00014>
    {
        public Kaizen00014Map()
        {
            // Primary Key
            this.HasKey(t => t.ConditionID);

            // Properties
            this.Property(t => t.FieldName)
                .HasMaxLength(50);

            this.Property(t => t.FieldValue)
                .HasMaxLength(50);

            this.Property(t => t.FieldCondition)
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("Kaizen00014");
            this.Property(t => t.ConditionID).HasColumnName("ConditionID");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.FieldValue).HasColumnName("FieldValue");
            this.Property(t => t.FieldCondition).HasColumnName("FieldCondition");
            this.Property(t => t.FieldOperatorAnd).HasColumnName("FieldOperatorAnd");

            // Relationships
            this.HasRequired(t => t.Kaizen00011)
                .WithMany(t => t.Kaizen00014)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
