using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00024Map : EntityTypeConfiguration<SOP00024>
    {
        public SOP00024Map()
        {
            // Primary Key
            this.HasKey(t => t.ShippingID);

            // Properties
            this.Property(t => t.ShippingID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ShippingName)
                .HasMaxLength(50);

            this.Property(t => t.Carrier)
                .HasMaxLength(50);

            this.Property(t => t.CarrierAccount)
                .HasMaxLength(50);

            this.Property(t => t.Contact)
                .HasMaxLength(50);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(50);

            this.Property(t => t.PhoneNumber02)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00024");
            this.Property(t => t.ShippingID).HasColumnName("ShippingID");
            this.Property(t => t.ShippingName).HasColumnName("ShippingName");
            this.Property(t => t.shippingtype).HasColumnName("shippingtype");
            this.Property(t => t.Carrier).HasColumnName("Carrier");
            this.Property(t => t.CarrierAccount).HasColumnName("CarrierAccount");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.PhoneNumber02).HasColumnName("PhoneNumber02");
        }
    }
}
