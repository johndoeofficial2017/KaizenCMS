using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10304Map : EntityTypeConfiguration<SOP10304>
    {
        public SOP10304Map()
        {
            // Primary Key
            this.HasKey(t => t.VoucherTrxNumber);

            // Properties
            this.Property(t => t.VoucherTrxNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP10304");
            this.Property(t => t.VoucherTrxNumber).HasColumnName("VoucherTrxNumber");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.DOCAMNT).HasColumnName("DOCAMNT");

            // Relationships
            this.HasRequired(t => t.SOP10300)
                .WithMany(t => t.SOP10304)
                .HasForeignKey(d => d.SOPNUMBE);

        }
    }
}
