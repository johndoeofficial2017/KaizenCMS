using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00063Map : EntityTypeConfiguration<CM00063>
    {
        public CM00063Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ViewID, t.CaseStatusID, t.FieldCode });

            // Properties
            this.Property(t => t.ViewID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CaseStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.FieldTitle)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00063");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.FieldCode).HasColumnName("FieldCode");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldOrder).HasColumnName("FieldOrder");
            this.Property(t => t.FieldTitle).HasColumnName("FieldTitle");

            // Relationships
            this.HasRequired(t => t.CM00062)
                .WithMany(t => t.CM00063)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
