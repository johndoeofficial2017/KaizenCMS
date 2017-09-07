using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00003Map : EntityTypeConfiguration<CM00003>
    {
        public CM00003Map()
        {
            // Primary Key
            this.HasKey(t => t.StatusActionTypeID);

            // Properties
            this.Property(t => t.StatusActionTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00003");
            this.Property(t => t.StatusActionTypeID).HasColumnName("StatusActionTypeID");
            this.Property(t => t.StatusActionTypeName).HasColumnName("StatusActionTypeName");
        }
    }
}
