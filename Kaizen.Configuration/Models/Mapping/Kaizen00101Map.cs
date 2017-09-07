using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00101Map : EntityTypeConfiguration<Kaizen00101>
    {
        public Kaizen00101Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CompanyID, t.ModuleID });

            // Properties
            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ModuleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Kaizen00101");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Kaizen00101)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
