using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00089Map : EntityTypeConfiguration<CM00089>
    {
        public CM00089Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CheckbookCode, t.CurrencyCode });

            // Properties
            this.Property(t => t.CheckbookCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CheckbookName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ToAgentID)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00089");
            this.Property(t => t.CheckbookCode).HasColumnName("CheckbookCode");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.CheckbookName).HasColumnName("CheckbookName");
            this.Property(t => t.TrxPaymentType).HasColumnName("TrxPaymentType");
            this.Property(t => t.ToAgent).HasColumnName("ToAgent");
            this.Property(t => t.ToAgentID).HasColumnName("ToAgentID");

            // Relationships
            this.HasRequired(t => t.CM00088)
                .WithMany(t => t.CM00089)
                .HasForeignKey(d => d.TrxPaymentType);

        }
    }
}
