using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CMV00202Map : EntityTypeConfiguration<CMV00202>
    {
        public CMV00202Map()
        {
            // Primary Key
            this.HasKey(t => t.CaseRef);

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseAccountNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CMV00202");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.CaseAccountNo).HasColumnName("CaseAccountNo");
            this.Property(t => t.GRADE).HasColumnName("GRADE");
        }
    }
}
