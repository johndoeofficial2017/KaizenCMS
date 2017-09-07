using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00014Map : EntityTypeConfiguration<CM00014>
    {
        public CM00014Map()
        {
            // Primary Key
            this.HasKey(t => t.DebtorStatusID);

            // Properties
            this.Property(t => t.DebtorStatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00014");
            this.Property(t => t.DebtorStatusID).HasColumnName("DebtorStatusID");
            this.Property(t => t.DebtorStatusName).HasColumnName("DebtorStatusName");
        }
    }
}
