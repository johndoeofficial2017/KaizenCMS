using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10107Map : EntityTypeConfiguration<SOP10107>
    {
        public SOP10107Map()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DocumentName)
                .HasMaxLength(10);

            this.Property(t => t.DocumentDescription)
                .HasMaxLength(50);

            this.Property(t => t.PhotoExtension)
                .IsRequired()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("SOP10107");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.DocumentDescription).HasColumnName("DocumentDescription");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");

            // Relationships
            this.HasRequired(t => t.SOP10100)
                .WithMany(t => t.SOP10107)
                .HasForeignKey(d => d.SOPNUMBE);

        }
    }
}
