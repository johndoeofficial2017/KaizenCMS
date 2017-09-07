using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10302Map : EntityTypeConfiguration<SOP10302>
    {
        public SOP10302Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CreditCardNumber)
                .HasMaxLength(50);

            this.Property(t => t.BankName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SOP10302");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.DOCAMNT).HasColumnName("DOCAMNT");
            this.Property(t => t.CreditCardNumber).HasColumnName("CreditCardNumber");
            this.Property(t => t.BankName).HasColumnName("BankName");

            // Relationships
            this.HasRequired(t => t.SOP10300)
                .WithMany(t => t.SOP10302)
                .HasForeignKey(d => d.SOPNUMBE);

        }
    }
}
