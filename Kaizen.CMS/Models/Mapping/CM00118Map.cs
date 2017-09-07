using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00118Map : EntityTypeConfiguration<CM00118>
    {
        public CM00118Map()
        {
            // Primary Key
            this.HasKey(t => new { t.UserName, t.DebtorID });

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("CM00118");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");

            // Relationships
            this.HasRequired(t => t.CM00100)
                .WithMany(t => t.CM00118)
                .HasForeignKey(d => d.DebtorID);

        }
    }
}
