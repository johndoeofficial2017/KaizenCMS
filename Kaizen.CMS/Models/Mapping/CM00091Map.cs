using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00091Map : EntityTypeConfiguration<CM00091>
    {
        public CM00091Map()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.CheckbookCode, t.CurrencyCode });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CheckbookCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00091");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.CheckbookCode).HasColumnName("CheckbookCode");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");

            // Relationships
            this.HasRequired(t => t.CM00089)
                .WithMany(t => t.CM00091)
                .HasForeignKey(d => new { d.CheckbookCode, d.CurrencyCode });

        }
    }
}
