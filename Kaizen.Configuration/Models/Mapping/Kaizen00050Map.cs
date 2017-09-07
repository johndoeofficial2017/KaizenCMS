using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00050Map : EntityTypeConfiguration<Kaizen00050>
    {
        public Kaizen00050Map()
        {
            // Primary Key
            this.HasKey(t => t.DashboardCode);

            // Properties
            this.Property(t => t.DashboardName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Kaizen00050");
            this.Property(t => t.DashboardCode).HasColumnName("DashboardCode");
            this.Property(t => t.DashboardName).HasColumnName("DashboardName");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Kaizen00050)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
