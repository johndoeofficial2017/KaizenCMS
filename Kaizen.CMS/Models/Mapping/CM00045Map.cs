using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00045Map : EntityTypeConfiguration<CM00045>
    {
        public CM00045Map()
        {
            // Primary Key
            this.HasKey(t => t.ActionPar);

            // Properties
            this.Property(t => t.ActionPar)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FunctionName)
                .HasMaxLength(50);

            this.Property(t => t.FieldName)
                .HasMaxLength(50);

            this.Property(t => t.FieldValue)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00045");
            this.Property(t => t.ActionPar).HasColumnName("ActionPar");
            this.Property(t => t.FunctionName).HasColumnName("FunctionName");
            this.Property(t => t.ActionID).HasColumnName("ActionID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.FieldValue).HasColumnName("FieldValue");

            // Relationships
            this.HasRequired(t => t.CM00044)
                .WithMany(t => t.CM00045)
                .HasForeignKey(d => d.ActionID);

        }
    }
}
