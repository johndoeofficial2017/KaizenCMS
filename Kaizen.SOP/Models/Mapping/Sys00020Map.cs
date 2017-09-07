using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
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

            // Table & Column Mappings
            this.ToTable("Sys00020");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");
            this.Property(t => t.PaymentMethodName).HasColumnName("PaymentMethodName");
        }
    }
}
