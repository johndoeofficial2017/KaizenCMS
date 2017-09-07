using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen006Map : EntityTypeConfiguration<Kaizen006>
    {
        public Kaizen006Map()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.CompanyID });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CheckbookCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Kaizen006");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.CheckbookCode).HasColumnName("CheckbookCode");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");

            // Relationships
            this.HasRequired(t => t.Kaizen00030)
                .WithMany(t => t.Kaizen006)
                .HasForeignKey(d => d.RoleID);

        }
    }
}
