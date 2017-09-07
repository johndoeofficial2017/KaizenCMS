using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00015Map : EntityTypeConfiguration<SOP00015>
    {
        public SOP00015Map()
        {
            // Primary Key
            this.HasKey(t => t.PaymentTermID);

            // Properties
            this.Property(t => t.PaymentTermID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PaymentTermName)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP00015");
            this.Property(t => t.PaymentTermID).HasColumnName("PaymentTermID");
            this.Property(t => t.PaymentTermName).HasColumnName("PaymentTermName");
        }
    }
}
