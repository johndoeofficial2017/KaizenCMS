using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM002030Map : EntityTypeConfiguration<CM002030>
    {
        public CM002030Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CaseRef, t.TRXTypeID });

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TRXTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM002030");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
        }
    }
}
