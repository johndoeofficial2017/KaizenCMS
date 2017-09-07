using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00112Map : EntityTypeConfiguration<CM00112>
    {
        public CM00112Map()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.DocumentName)
                .HasMaxLength(25);

            this.Property(t => t.DocumentDescription)
                .HasMaxLength(50);

            this.Property(t => t.PhotoExtension)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00112");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.DocumentDescription).HasColumnName("DocumentDescription");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");

            // Relationships
            this.HasRequired(t => t.CM00110)
                .WithMany(t => t.CM00112)
                .HasForeignKey(d => d.ClientID);
        }
    }
}
