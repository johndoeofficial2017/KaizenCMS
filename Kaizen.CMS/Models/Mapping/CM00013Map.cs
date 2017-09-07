using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00013Map : EntityTypeConfiguration<CM00013>
    {
        public CM00013Map()
        {
            // Primary Key
            this.HasKey(t => t.BillingFrequencyID);

            // Properties
            this.Property(t => t.BillingFrequencyName)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00013");
            this.Property(t => t.BillingFrequencyID).HasColumnName("BillingFrequencyID");
            this.Property(t => t.BillingFrequencyName).HasColumnName("BillingFrequencyName");
        }
    }
}
