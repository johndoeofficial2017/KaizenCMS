using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00103Map : EntityTypeConfiguration<Sys00103>
    {
        public Sys00103Map()
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
            this.ToTable("Sys00103");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.DocTypeID).HasColumnName("DocTypeID");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.DocumentDescription).HasColumnName("DocumentDescription");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");

            // Relationships
            this.HasRequired(t => t.Sys00100)
                .WithMany(t => t.Sys00103)
                .HasForeignKey(d => d.TaskID);
            this.HasRequired(t => t.Sys00104)
                .WithMany(t => t.Sys00103)
                .HasForeignKey(d => d.DocTypeID);

        }
    }
}
