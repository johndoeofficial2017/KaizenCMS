using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00004Map : EntityTypeConfiguration<CM00004>
    {
        public CM00004Map()
        {
            // Primary Key
            this.HasKey(t => t.TaskTypeID);

            // Properties
            this.Property(t => t.TaskTypeID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TaskTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00004");
            this.Property(t => t.TaskTypeID).HasColumnName("TaskTypeID");
            this.Property(t => t.TaskTypeName).HasColumnName("TaskTypeName");
        }
    }
}
