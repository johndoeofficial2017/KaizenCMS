using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00054Map : EntityTypeConfiguration<Kaizen00054>
    {
        public Kaizen00054Map()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.DashboardCode });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DashboardCode)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Kaizen00054");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.DashboardCode).HasColumnName("DashboardCode");

            // Relationships
            this.HasRequired(t => t.Kaizen00050)
                .WithMany(t => t.Kaizen00054)
                .HasForeignKey(d => d.DashboardCode);

        }
    }
}
