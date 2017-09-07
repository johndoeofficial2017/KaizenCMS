using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00061Map : EntityTypeConfiguration<CM00061>
    {
        public CM00061Map()
        {
            // Primary Key
            this.HasKey(t => new { t.FieldCode, t.CaseStatusID, t.LookupID });

            // Properties
            this.Property(t => t.FieldCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.CaseStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LookupID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LookupName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00061");
            this.Property(t => t.FieldCode).HasColumnName("FieldCode");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.LookupID).HasColumnName("LookupID");
            this.Property(t => t.LookupName).HasColumnName("LookupName");

            // Relationships
            this.HasRequired(t => t.CM00060)
                .WithMany(t => t.CM00061)
                .HasForeignKey(d => new { d.CaseStatusID, d.FieldCode });

        }
    }
}
