using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00056Map : EntityTypeConfiguration<CM00056>
    {
        public CM00056Map()
        {
            // Primary Key
            this.HasKey(t => new { t.TRXTypeID, t.RoleID });

            // Properties
            this.Property(t => t.TRXTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM00056");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");

            // Relationships
            this.HasRequired(t => t.CM00029)
                .WithMany(t => t.CM00056)
                .HasForeignKey(d => d.TRXTypeID);

        }
    }
}
