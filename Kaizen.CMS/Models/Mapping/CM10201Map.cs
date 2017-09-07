using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM10201Map : EntityTypeConfiguration<CM10201>
    {
        public CM10201Map()
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

            // Table & Column Mappings
            this.ToTable("CM10201");
            this.Property(t => t.CIFNumber).HasColumnName("CIFNumber");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");

            // Relationships
            this.HasRequired(t => t.CM00100)
                .WithMany(t => t.CM10201)
                .HasForeignKey(d => d.DebtorID);

        }
    }
}
