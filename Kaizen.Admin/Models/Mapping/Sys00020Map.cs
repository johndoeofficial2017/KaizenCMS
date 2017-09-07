using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00020Map : EntityTypeConfiguration<Sys00020>
    {
        public Sys00020Map()
        {
            // Primary Key
            this.HasKey(t => t.PaymentMethodID);

            // Properties
            this.Property(t => t.PaymentMethodID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PaymentMethodName)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ReceiptPrefix)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("Sys00020");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");
            this.Property(t => t.PaymentMethodName).HasColumnName("PaymentMethodName");
            this.Property(t => t.ReceiptPrefix).HasColumnName("ReceiptPrefix");
            this.Property(t => t.ReceiptLengh).HasColumnName("ReceiptLengh");
            this.Property(t => t.ReceiptLast).HasColumnName("ReceiptLast");
        }
    }
}
