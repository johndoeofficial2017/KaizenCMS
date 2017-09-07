using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00060Map : EntityTypeConfiguration<CM00060>
    {
        public CM00060Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CaseStatusID, t.FieldCode });

            // Properties
            this.Property(t => t.CaseStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.FieldName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00060");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.FieldCode).HasColumnName("FieldCode");
            this.Property(t => t.IsRequired).HasColumnName("IsRequired");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");

            // Relationships
            this.HasRequired(t => t.CM00028)
                .WithMany(t => t.CM00060)
                .HasForeignKey(d => d.FieldTypeID);
            this.HasRequired(t => t.CM00700)
                .WithMany(t => t.CM00060)
                .HasForeignKey(d => d.CaseStatusID);

        }
    }
}
