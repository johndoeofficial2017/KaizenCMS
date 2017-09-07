using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class CRM00201Map : EntityTypeConfiguration<CRM00201>
    {
        public CRM00201Map()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.DocumentName)
                .HasMaxLength(50);

            this.Property(t => t.PhotoExtension)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CRM00201");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.TransactionID).HasColumnName("TransactionID");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");

            // Relationships
            this.HasRequired(t => t.CRM00200)
                .WithMany(t => t.CRM00201)
                .HasForeignKey(d => d.TransactionID);

        }
    }
}
