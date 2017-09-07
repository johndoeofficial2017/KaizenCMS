using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00004Map : EntityTypeConfiguration<Sys00004>
    {
        public Sys00004Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ModuleID, t.CompanyID, t.UserCode });

            // Properties
            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.UserCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("Sys00004");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.UserCode).HasColumnName("UserCode");

            // Relationships
            this.HasRequired(t => t.Sys00003)
                .WithMany(t => t.Sys00004)
                .HasForeignKey(d => d.ModuleID);

        }
    }
}
