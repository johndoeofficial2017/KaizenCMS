using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00504Map : EntityTypeConfiguration<SOP00504>
    {
        public SOP00504Map()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.TrxDocumentID)
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
            this.ToTable("SOP00504");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.TrxDocumentID).HasColumnName("TrxDocumentID");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.DocumentDescription).HasColumnName("DocumentDescription");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");

            // Relationships
            this.HasRequired(t => t.SOP00500)
                .WithMany(t => t.SOP00504)
                .HasForeignKey(d => d.TrxDocumentID);

        }
    }
}
