using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00054Map : EntityTypeConfiguration<CM00054>
    {
        public CM00054Map()
        {
            // Primary Key
            this.HasKey(t => new { t.StatusActionTypeID, t.RoleID });

            // Properties
            this.Property(t => t.StatusActionTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM00054");
            this.Property(t => t.StatusActionTypeID).HasColumnName("StatusActionTypeID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");

            // Relationships
            this.HasRequired(t => t.CM00003)
                .WithMany(t => t.CM00054)
                .HasForeignKey(d => d.StatusActionTypeID);

        }
    }
}
