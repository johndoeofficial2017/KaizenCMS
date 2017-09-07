using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00119Map : EntityTypeConfiguration<CM00119>
    {
        public CM00119Map()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.DebtorID });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("CM00119");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");

            // Relationships
            this.HasRequired(t => t.CM00100)
                .WithMany(t => t.CM00119)
                .HasForeignKey(d => d.DebtorID);

        }
    }
}
