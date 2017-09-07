using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00048Map : EntityTypeConfiguration<CM00048>
    {
        public CM00048Map()
        {
            // Primary Key
            this.HasKey(t => t.TRXTypeID);

            // Properties
            this.Property(t => t.TRXTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM00048");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.HtmlNew).HasColumnName("HtmlNew");

            // Relationships
            this.HasRequired(t => t.CM00029)
                .WithOptional(t => t.CM00048);

        }
    }
}
