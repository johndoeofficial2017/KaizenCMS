using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM20201Map : EntityTypeConfiguration<CM20201>
    {
        public CM20201Map()
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
            this.ToTable("CM20201");
            this.Property(t => t.CIFNumber).HasColumnName("CIFNumber");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.EnglishFullName).HasColumnName("EnglishFullName");

            // Relationships
            this.HasRequired(t => t.CM20200)
                .WithMany(t => t.CM20201)
                .HasForeignKey(d => d.CIFNumber);

        }
    }
}
