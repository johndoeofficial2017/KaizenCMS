using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class CRM00202Map : EntityTypeConfiguration<CRM00202>
    {
        public CRM00202Map()
        {
            // Primary Key
            this.HasKey(t => t.ReceiverID);

            // Properties
            this.Property(t => t.ReceiverEmail)
                .HasMaxLength(75);

            // Table & Column Mappings
            this.ToTable("CRM00202");
            this.Property(t => t.ReceiverID).HasColumnName("ReceiverID");
            this.Property(t => t.TransactionID).HasColumnName("TransactionID");
            this.Property(t => t.ReceiverEmail).HasColumnName("ReceiverEmail");

            // Relationships
            this.HasRequired(t => t.CRM00200)
                .WithMany(t => t.CRM00202)
                .HasForeignKey(d => d.TransactionID);

        }
    }
}
