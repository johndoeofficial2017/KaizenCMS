using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00015Map : EntityTypeConfiguration<CM00015>
    {
        public CM00015Map()
        {
            // Primary Key
            this.HasKey(t => t.BucketID);

            // Properties
            this.Property(t => t.BucketID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BucketName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00015");
            this.Property(t => t.BucketID).HasColumnName("BucketID");
            this.Property(t => t.BucketName).HasColumnName("BucketName");
        }
    }
}
