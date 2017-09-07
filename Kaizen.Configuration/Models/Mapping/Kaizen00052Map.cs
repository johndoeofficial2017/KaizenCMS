using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00052Map : EntityTypeConfiguration<Kaizen00052>
    {
        public Kaizen00052Map()
        {
            // Primary Key
            this.HasKey(t => t.ReportID);

            // Properties
            this.Property(t => t.ReportID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ReportName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen00052");
            this.Property(t => t.ReportID).HasColumnName("ReportID");
            this.Property(t => t.ReportTypeID).HasColumnName("ReportTypeID");
            this.Property(t => t.ReportName).HasColumnName("ReportName");

            // Relationships
            this.HasRequired(t => t.Kaizen00051)
                .WithMany(t => t.Kaizen00052)
                .HasForeignKey(d => d.ReportTypeID);

        }
    }
}
