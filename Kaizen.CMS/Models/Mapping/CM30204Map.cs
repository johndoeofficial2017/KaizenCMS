using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM30204Map : EntityTypeConfiguration<CM30204>
    {
        public CM30204Map()
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
            this.ToTable("CM30204");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");

            // Relationships
            this.HasRequired(t => t.CM30207)
                .WithMany(t => t.CM30204)
                .HasForeignKey(d => d.PaymentID);

        }
    }
}
