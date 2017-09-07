using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen10200Map : EntityTypeConfiguration<Kaizen10200>
    {
        public Kaizen10200Map()
        {
            // Primary Key
            this.HasKey(t => t.TransactionID);

            // Properties
            this.Property(t => t.PhoneFrom)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PhoneTo)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen10200");
            this.Property(t => t.TransactionID).HasColumnName("TransactionID");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.PhoneFrom).HasColumnName("PhoneFrom");
            this.Property(t => t.PhoneTo).HasColumnName("PhoneTo");
            this.Property(t => t.IsPhoneIN).HasColumnName("IsPhoneIN");
        }
    }
}
