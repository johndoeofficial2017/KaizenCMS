using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00050Map : EntityTypeConfiguration<CM00050>
    {
        public CM00050Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.FieldName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.LookupFieldValue)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00050");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.LookupFieldValue).HasColumnName("LookupFieldValue");

            // Relationships
            this.HasRequired(t => t.CM00070)
                .WithMany(t => t.CM00050)
                .HasForeignKey(d => new { d.TRXTypeID, d.FieldName });

        }
    }
}
