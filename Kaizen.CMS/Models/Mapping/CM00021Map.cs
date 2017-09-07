using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00021Map : EntityTypeConfiguration<CM00021>
    {
        public CM00021Map()
        {
            // Primary Key
            this.HasKey(t => t.CUSTCLAS);

            // Properties
            this.Property(t => t.CUSTCLAS)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTCLASNAME)
                .HasMaxLength(50);

            this.Property(t => t.PrefixValue)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CM00021");
            this.Property(t => t.CUSTCLAS).HasColumnName("CUSTCLAS");
            this.Property(t => t.CUSTCLASNAME).HasColumnName("CUSTCLASNAME");
            this.Property(t => t.PrefixValue).HasColumnName("PrefixValue");
            this.Property(t => t.Prefixlengh).HasColumnName("Prefixlengh");
            this.Property(t => t.LastClientID).HasColumnName("LastClientID");
        }
    }
}
