using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10108Map : EntityTypeConfiguration<SOP10108>
    {
        public SOP10108Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.LineID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SourceID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LineReference)
                .HasMaxLength(100);

            this.Property(t => t.KaizenID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("SOP10108");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.SourceID).HasColumnName("SourceID");
            this.Property(t => t.DEBITAMT).HasColumnName("DEBITAMT");
            this.Property(t => t.CRDTAMNT).HasColumnName("CRDTAMNT");
            this.Property(t => t.ORDBTAMT).HasColumnName("ORDBTAMT");
            this.Property(t => t.ORCRDAMT).HasColumnName("ORCRDAMT");
            this.Property(t => t.LineReference).HasColumnName("LineReference");
            this.Property(t => t.LineDescription).HasColumnName("LineDescription");
            this.Property(t => t.KaizenID).HasColumnName("KaizenID");

            // Relationships
            this.HasRequired(t => t.SOP10100)
                .WithMany(t => t.SOP10108)
                .HasForeignKey(d => d.SOPNUMBE);

        }
    }
}
