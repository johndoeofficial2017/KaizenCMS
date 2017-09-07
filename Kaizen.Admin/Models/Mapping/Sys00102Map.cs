using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00102Map : EntityTypeConfiguration<Sys00102>
    {
        public Sys00102Map()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.DocumentName)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.DocumentDescription)
                .HasMaxLength(50);

            this.Property(t => t.PhotoExtension)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("Sys00102");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.ActionID).HasColumnName("ActionID");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.DocumentDescription).HasColumnName("DocumentDescription");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");

            // Relationships
            this.HasRequired(t => t.Sys00101)
                .WithMany(t => t.Sys00102)
                .HasForeignKey(d => d.ActionID);

        }
    }
}
