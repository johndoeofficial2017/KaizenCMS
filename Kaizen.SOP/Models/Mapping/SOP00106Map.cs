using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00106Map : EntityTypeConfiguration<SOP00106>
    {
        public SOP00106Map()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DocumentName)
                .HasMaxLength(25);

            this.Property(t => t.DocumentDescription)
                .HasMaxLength(50);

            this.Property(t => t.PhotoExtension)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP00106");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.DocumentDescription).HasColumnName("DocumentDescription");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");

            // Relationships
            this.HasRequired(t => t.SOP00100)
                .WithMany(t => t.SOP00106)
                .HasForeignKey(d => d.CUSTNMBR);
            this.HasRequired(t => t.Sys00007)
                .WithMany(t => t.SOP00106)
                .HasForeignKey(d => d.DocumentTypeID);

        }
    }
}
