using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00301Map : EntityTypeConfiguration<SOP00301>
    {
        public SOP00301Map()
        {
            // Primary Key
            this.HasKey(t => t.SOPTypeSetupID);

            // Properties
            this.Property(t => t.SOPTypeSetupID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SOPTypeSetupName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00301");
            this.Property(t => t.SOPTypeSetupID).HasColumnName("SOPTypeSetupID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.SOPTypeSetupName).HasColumnName("SOPTypeSetupName");

            // Relationships
            this.HasRequired(t => t.SOP00300)
                .WithMany(t => t.SOP00301)
                .HasForeignKey(d => d.CategoryID);

        }
    }
}
