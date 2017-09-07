using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM30201Map : EntityTypeConfiguration<CM30201>
    {
        public CM30201Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CIFNumber, t.DebtorID });

            // Properties
            this.Property(t => t.CIFNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.EnglishFullName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CM30201");
            this.Property(t => t.CIFNumber).HasColumnName("CIFNumber");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.EnglishFullName).HasColumnName("EnglishFullName");

            // Relationships
            this.HasRequired(t => t.CM30200)
                .WithMany(t => t.CM30201)
                .HasForeignKey(d => d.CIFNumber);

        }
    }
}
