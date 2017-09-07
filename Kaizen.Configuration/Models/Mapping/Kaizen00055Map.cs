using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00055Map : EntityTypeConfiguration<Kaizen00055>
    {
        public Kaizen00055Map()
        {
            // Primary Key
            this.HasKey(t => new { t.UserName, t.DashboardCode });

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DashboardCode)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Kaizen00055");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.DashboardCode).HasColumnName("DashboardCode");

            // Relationships
            this.HasRequired(t => t.Kaizen00050)
                .WithMany(t => t.Kaizen00055)
                .HasForeignKey(d => d.DashboardCode);

        }
    }
}
