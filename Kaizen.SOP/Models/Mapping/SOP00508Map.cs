using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00508Map : EntityTypeConfiguration<SOP00508>
    {
        public SOP00508Map()
        {
            // Primary Key
            this.HasKey(t => t.AccountLine);

            // Properties
            this.Property(t => t.AccountLine)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

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
            this.ToTable("SOP00508");
            this.Property(t => t.AccountLine).HasColumnName("AccountLine");
            this.Property(t => t.TrxDocumentID).HasColumnName("TrxDocumentID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.ACTNUMBR).HasColumnName("ACTNUMBR");
            this.Property(t => t.AccountName).HasColumnName("AccountName");
            this.Property(t => t.SourceID).HasColumnName("SourceID");
            this.Property(t => t.DebitAMT).HasColumnName("DebitAMT");
            this.Property(t => t.CreditAMT).HasColumnName("CreditAMT");
            this.Property(t => t.GLReference).HasColumnName("GLReference");

            // Relationships
            this.HasRequired(t => t.SOP00500)
                .WithMany(t => t.SOP00508)
                .HasForeignKey(d => d.TrxDocumentID);

        }
    }
}
