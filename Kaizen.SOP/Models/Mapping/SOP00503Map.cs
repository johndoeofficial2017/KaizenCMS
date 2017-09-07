using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00503Map : EntityTypeConfiguration<SOP00503>
    {
        public SOP00503Map()
        {
            // Primary Key
            this.HasKey(t => new { t.SOPNUMBE, t.SOPTypeSetupID, t.SOPTYPE, t.TrxDocumentID });

            // Properties
            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SOPTypeSetupID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TrxDocumentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CustomerPoNumber)
                .HasMaxLength(15);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP00503");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.SOPTypeSetupID).HasColumnName("SOPTypeSetupID");
            this.Property(t => t.SOPTYPE).HasColumnName("SOPTYPE");
            this.Property(t => t.TrxDocumentID).HasColumnName("TrxDocumentID");
            this.Property(t => t.DOCDATE).HasColumnName("DOCDATE");
            this.Property(t => t.CustomerPoNumber).HasColumnName("CustomerPoNumber");
            this.Property(t => t.DOCAMNT).HasColumnName("DOCAMNT");
            this.Property(t => t.ORGAplyAMT).HasColumnName("ORGAplyAMT");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.WriteOff).HasColumnName("WriteOff");

            // Relationships
            this.HasRequired(t => t.SOP00500)
                .WithMany(t => t.SOP00503)
                .HasForeignKey(d => d.TrxDocumentID);

        }
    }
}
