using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00002Map : EntityTypeConfiguration<CM00002>
    {
        public CM00002Map()
        {
            // Primary Key
            this.HasKey(t => t.PartnerClassID);

            // Properties
            this.Property(t => t.PartnerClassID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PartnerClassName)
                .HasMaxLength(50);

            this.Property(t => t.PrefixValue)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CM00002");
            this.Property(t => t.PartnerClassID).HasColumnName("PartnerClassID");
            this.Property(t => t.PartnerClassName).HasColumnName("PartnerClassName");
            this.Property(t => t.PrefixValue).HasColumnName("PrefixValue");
            this.Property(t => t.Prefixlengh).HasColumnName("Prefixlengh");
            this.Property(t => t.LastPartnerID).HasColumnName("LastPartnerID");
        }
    }
}
