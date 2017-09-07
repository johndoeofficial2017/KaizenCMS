using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00018Map : EntityTypeConfiguration<CM00018>
    {
        public CM00018Map()
        {
            // Primary Key
            this.HasKey(t => t.PaymentBaseID);

            // Properties
            this.Property(t => t.PaymentBaseID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PaymentBaseName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00018");
            this.Property(t => t.PaymentBaseID).HasColumnName("PaymentBaseID");
            this.Property(t => t.PaymentBaseName).HasColumnName("PaymentBaseName");
        }
    }
}
