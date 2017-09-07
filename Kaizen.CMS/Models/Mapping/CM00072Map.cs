using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00072Map : EntityTypeConfiguration<CM00072>
    {
        public CM00072Map()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.ViewID });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ViewID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM00072");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.ViewID).HasColumnName("ViewID");

            // Relationships
            this.HasRequired(t => t.CM00071)
                .WithMany(t => t.CM00072)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
