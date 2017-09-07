using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class KaizenSequenceMap : EntityTypeConfiguration<KaizenSequence>
    {
        public KaizenSequenceMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CompanyID, t.UserName, t.SequenceName });

            // Properties
            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SequenceName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("KaizenSequence");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.SequenceName).HasColumnName("SequenceName");
            this.Property(t => t.KaizenID).HasColumnName("KaizenID");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.KaizenSequences)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
