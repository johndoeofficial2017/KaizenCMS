using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10203Map : EntityTypeConfiguration<SOP10203>
    {
        public SOP10203Map()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.DocumentName)
                .HasMaxLength(25);

            this.Property(t => t.DocumentDescription)
                .HasMaxLength(50);

            this.Property(t => t.PhotoExtension)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP10203");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.DocumentDescription).HasColumnName("DocumentDescription");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");

            // Relationships
            this.HasRequired(t => t.SOP10201)
                .WithMany(t => t.SOP10203)
                .HasForeignKey(d => d.LineID);

        }
    }
}
