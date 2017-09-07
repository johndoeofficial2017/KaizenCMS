using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00090Map : EntityTypeConfiguration<CM00090>
    {
        public CM00090Map()
        {
            // Primary Key
            this.HasKey(t => new { t.UserName, t.CheckbookCode, t.CurrencyCode });

            // Properties
            this.Property(t => t.UserName)
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
            this.ToTable("CM00090");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.CheckbookCode).HasColumnName("CheckbookCode");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");

            // Relationships
            this.HasRequired(t => t.CM00089)
                .WithMany(t => t.CM00090)
                .HasForeignKey(d => new { d.CheckbookCode, d.CurrencyCode });

        }
    }
}
