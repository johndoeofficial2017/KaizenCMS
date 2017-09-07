using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00507Map : EntityTypeConfiguration<SOP00507>
    {
        public SOP00507Map()
        {
            // Primary Key
            this.HasKey(t => t.AccountLine);

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

            this.Property(t => t.ACTNUMBR)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AccountName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SourceID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.GLReference)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00507");
            this.Property(t => t.AccountLine).HasColumnName("AccountLine");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.SOPTypeSetupID).HasColumnName("SOPTypeSetupID");
            this.Property(t => t.SOPTYPE).HasColumnName("SOPTYPE");
            this.Property(t => t.TrxDocumentID).HasColumnName("TrxDocumentID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.ACTNUMBR).HasColumnName("ACTNUMBR");
            this.Property(t => t.AccountName).HasColumnName("AccountName");
            this.Property(t => t.SourceID).HasColumnName("SourceID");
            this.Property(t => t.DebitAMT).HasColumnName("DebitAMT");
            this.Property(t => t.CreditAMT).HasColumnName("CreditAMT");
            this.Property(t => t.DebitApplyAMT).HasColumnName("DebitApplyAMT");
            this.Property(t => t.CrebitApplyAMT).HasColumnName("CrebitApplyAMT");
            this.Property(t => t.GLReference).HasColumnName("GLReference");

            // Relationships
            this.HasRequired(t => t.SOP00503)
                .WithMany(t => t.SOP00507)
                .HasForeignKey(d => new { d.SOPNUMBE, d.SOPTypeSetupID, d.SOPTYPE, d.TrxDocumentID });

        }
    }
}
