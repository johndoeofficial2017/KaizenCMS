using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00088Map : EntityTypeConfiguration<CM00088>
    {
        public CM00088Map()
        {
            // Primary Key
            this.HasKey(t => t.TrxPaymentType);

            // Properties
            this.Property(t => t.TrxPaymentType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PaymentTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00088");
            this.Property(t => t.TrxPaymentType).HasColumnName("TrxPaymentType");
            this.Property(t => t.PaymentTypeName).HasColumnName("PaymentTypeName");
        }
    }
}
