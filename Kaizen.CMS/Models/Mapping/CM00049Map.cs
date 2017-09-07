using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00049Map : EntityTypeConfiguration<CM00049>
    {
        public CM00049Map()
        {
            // Primary Key
            this.HasKey(t => t.TRXTypeID);

            // Properties
            this.Property(t => t.TRXTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM00049");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.HTMEdit).HasColumnName("HTMEdit");

            // Relationships
            this.HasRequired(t => t.CM00029)
                .WithOptional(t => t.CM00049);

        }
    }
}
