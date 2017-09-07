using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00040Map : EntityTypeConfiguration<CM00040>
    {
        public CM00040Map()
        {
            // Primary Key
            this.HasKey(t => t.GraphTypeID);

            // Properties
            this.Property(t => t.GraphTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.GraphTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00040");
            this.Property(t => t.GraphTypeID).HasColumnName("GraphTypeID");
            this.Property(t => t.GraphTypeName).HasColumnName("GraphTypeName");
        }
    }
}
