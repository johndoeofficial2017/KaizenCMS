using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00170Map : EntityTypeConfiguration<CM00170>
    {
        public CM00170Map()
        {
            // Primary Key
            this.HasKey(t => t.CreditorID);

            // Properties
            this.Property(t => t.CreditorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.CreditorName)
                .HasMaxLength(50);

            this.Property(t => t.ShortName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00170");
            this.Property(t => t.CreditorID).HasColumnName("CreditorID");
            this.Property(t => t.CreditorName).HasColumnName("CreditorName");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
        }
    }
}
