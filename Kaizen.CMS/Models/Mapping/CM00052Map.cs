using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00052Map : EntityTypeConfiguration<CM00052>
    {
        public CM00052Map()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.CaseStatusID });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CaseStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM00052");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");

            // Relationships
            this.HasRequired(t => t.CM00700)
                .WithMany(t => t.CM00052)
                .HasForeignKey(d => d.CaseStatusID);

        }
    }
}
