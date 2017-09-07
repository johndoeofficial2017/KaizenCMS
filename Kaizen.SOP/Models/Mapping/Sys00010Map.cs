using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class Sys00010Map : EntityTypeConfiguration<Sys00010>
    {
        public Sys00010Map()
        {
            // Primary Key
            this.HasKey(t => t.PaymentType);

            // Properties
            this.Property(t => t.PaymentType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PaymentTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sys00010");
            this.Property(t => t.PaymentType).HasColumnName("PaymentType");
            this.Property(t => t.PaymentTypeName).HasColumnName("PaymentTypeName");
        }
    }
}
