using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00066Map : EntityTypeConfiguration<CM00066>
    {
        public CM00066Map()
        {
            // Primary Key
            this.HasKey(t => t.LegalClassID);

            // Properties
            this.Property(t => t.LegalClassID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LegalClassName)
                .HasMaxLength(50);

            this.Property(t => t.PrefixValue)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CM00066");
            this.Property(t => t.LegalClassID).HasColumnName("LegalClassID");
            this.Property(t => t.LegalClassName).HasColumnName("LegalClassName");
            this.Property(t => t.PrefixValue).HasColumnName("PrefixValue");
            this.Property(t => t.Prefixlengh).HasColumnName("Prefixlengh");
            this.Property(t => t.LastLegalID).HasColumnName("LastLegalID");
        }
    }
}
