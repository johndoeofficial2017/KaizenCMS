using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00004Map : EntityTypeConfiguration<SOP00004>
    {
        public SOP00004Map()
        {
            // Primary Key
            this.HasKey(t => t.BatchID);

            // Properties
            this.Property(t => t.BatchID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BatchName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00004");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.BatchName).HasColumnName("BatchName");
            this.Property(t => t.SOPTYPE).HasColumnName("SOPTYPE");

            // Relationships
            this.HasRequired(t => t.SOP00000)
                .WithMany(t => t.SOP00004)
                .HasForeignKey(d => d.SOPTYPE);

        }
    }
}
