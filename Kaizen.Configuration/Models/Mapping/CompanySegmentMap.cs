using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class CompanySegmentMap : EntityTypeConfiguration<CompanySegment>
    {
        public CompanySegmentMap()
        {
            // Primary Key
            this.HasKey(t => t.SegmentID);

            // Properties
            this.Property(t => t.SegmentName)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CompanySegment");
            this.Property(t => t.SegmentID).HasColumnName("SegmentID");
            this.Property(t => t.SegmentName).HasColumnName("SegmentName");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.SegmentLength).HasColumnName("SegmentLength");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.CompanySegments)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
