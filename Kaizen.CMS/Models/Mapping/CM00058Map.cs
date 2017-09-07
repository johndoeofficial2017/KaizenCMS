using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00058Map : EntityTypeConfiguration<CM00058>
    {
        public CM00058Map()
        {
            // Primary Key
            this.HasKey(t => t.TargetTypeID);

            // Properties
            this.Property(t => t.TargetTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TargetTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00058");
            this.Property(t => t.TargetTypeID).HasColumnName("TargetTypeID");
            this.Property(t => t.TargetTypeName).HasColumnName("TargetTypeName");
        }
    }
}
