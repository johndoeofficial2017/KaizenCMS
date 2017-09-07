using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00032Map : EntityTypeConfiguration<CM00032>
    {
        public CM00032Map()
        {
            // Primary Key
            this.HasKey(t => new { t.AddressCode, t.RoleID });

            // Properties
            this.Property(t => t.AddressCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM00032");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.RoleID).HasColumnName("RoleID");

            // Relationships
            this.HasRequired(t => t.CM00009)
                .WithMany(t => t.CM00032)
                .HasForeignKey(d => d.AddressCode);

        }
    }
}
