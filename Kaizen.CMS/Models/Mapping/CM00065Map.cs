using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00065Map : EntityTypeConfiguration<CM00065>
    {
        public CM00065Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ViewID, t.RoleID });

            // Properties
            this.Property(t => t.ViewID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM00065");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");

            // Relationships
            this.HasRequired(t => t.CM00062)
                .WithMany(t => t.CM00065)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
