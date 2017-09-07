using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CMV00201Map : EntityTypeConfiguration<CMV00201>
    {
        public CMV00201Map()
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

            this.Property(t => t.BucketName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CMV00201");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.CaseAccountNo).HasColumnName("CaseAccountNo");
            this.Property(t => t.BucketID).HasColumnName("BucketID");
            this.Property(t => t.BucketName).HasColumnName("BucketName");
        }
    }
}
