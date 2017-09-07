using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10502Map : EntityTypeConfiguration<SOP10502>
    {
        public SOP10502Map()
        {
            // Primary Key
            this.HasKey(t => t.LotRowID);

            // Properties
            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BarCode)
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("SOP10502");
            this.Property(t => t.LotRowID).HasColumnName("LotRowID");
            this.Property(t => t.ItemLineIndex).HasColumnName("ItemLineIndex");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.BarCode).HasColumnName("BarCode");
            this.Property(t => t.LOTLineCode).HasColumnName("LOTLineCode");
            this.Property(t => t.AppliedQuantity).HasColumnName("AppliedQuantity");

            // Relationships
            this.HasRequired(t => t.SOP10501)
                .WithMany(t => t.SOP10502)
                .HasForeignKey(d => new { d.ItemLineIndex, d.SOPNUMBE });

        }
    }
}
