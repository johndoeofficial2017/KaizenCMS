using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00028Map : EntityTypeConfiguration<CM00028>
    {
        public CM00028Map()
        {
            // Primary Key
            this.HasKey(t => t.FieldTypeID);

            // Properties
            this.Property(t => t.FieldTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00028");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldTypeName).HasColumnName("FieldTypeName");
        }
    }
}
