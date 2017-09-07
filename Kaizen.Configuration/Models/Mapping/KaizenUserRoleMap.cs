using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class KaizenUserRoleMap : EntityTypeConfiguration<KaizenUserRole>
    {
        public KaizenUserRoleMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.UserName });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("KaizenUserRole");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.UserName).HasColumnName("UserName");

            // Relationships
            this.HasRequired(t => t.Kaizen00030)
                .WithMany(t => t.KaizenUserRoles)
                .HasForeignKey(d => d.RoleID);

        }
    }
}
