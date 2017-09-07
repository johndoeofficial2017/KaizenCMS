using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00051Map : EntityTypeConfiguration<Kaizen00051>
    {
        public Kaizen00051Map()
        {
            // Primary Key
            this.HasKey(t => t.ReportTypeID);

            // Properties
            this.Property(t => t.ReportTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ReportTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen00051");
            this.Property(t => t.ReportTypeID).HasColumnName("ReportTypeID");
            this.Property(t => t.ReportTypeName).HasColumnName("ReportTypeName");
        }
    }
}
