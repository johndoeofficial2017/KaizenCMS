using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00308Map : EntityTypeConfiguration<CM00308>
    {
        public CM00308Map()
        {
            // Primary Key
            this.HasKey(t => new { t.PaymentID, t.CaseRef });

            // Properties
            this.Property(t => t.PaymentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00308");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.Amount).HasColumnName("Amount");

            // Relationships
            this.HasRequired(t => t.CM00307)
                .WithMany(t => t.CM00308)
                .HasForeignKey(d => d.PaymentID);

        }
    }
}
