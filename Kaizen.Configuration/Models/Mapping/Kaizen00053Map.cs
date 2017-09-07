using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00053Map : EntityTypeConfiguration<Kaizen00053>
    {
        public Kaizen00053Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ReportID, t.DashboardCode });

            // Properties
            this.Property(t => t.ReportID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DashboardCode)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Kaizen00053");
            this.Property(t => t.ReportID).HasColumnName("ReportID");
            this.Property(t => t.DashboardCode).HasColumnName("DashboardCode");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");

            // Relationships
            this.HasRequired(t => t.Kaizen00050)
                .WithMany(t => t.Kaizen00053)
                .HasForeignKey(d => d.DashboardCode);
            this.HasRequired(t => t.Kaizen00052)
                .WithMany(t => t.Kaizen00053)
                .HasForeignKey(d => d.ReportID);

        }
    }
}
