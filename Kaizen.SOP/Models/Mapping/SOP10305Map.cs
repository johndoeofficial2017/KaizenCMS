using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10305Map : EntityTypeConfiguration<SOP10305>
    {
        public SOP10305Map()
        {
            // Primary Key
            this.HasKey(t => t.VoucherTrxNumber);

            // Properties
            this.Property(t => t.VoucherTrxNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BarcodPrefix)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.TrxComments)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("SOP10305");
            this.Property(t => t.VoucherTrxNumber).HasColumnName("VoucherTrxNumber");
            this.Property(t => t.VoucherAMT).HasColumnName("VoucherAMT");
            this.Property(t => t.VoucherCount).HasColumnName("VoucherCount");
            this.Property(t => t.VoucherStartDate).HasColumnName("VoucherStartDate");
            this.Property(t => t.VoucherEndDate).HasColumnName("VoucherEndDate");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.EntryDate).HasColumnName("EntryDate");
            this.Property(t => t.BarcodPrefix).HasColumnName("BarcodPrefix");
            this.Property(t => t.BarcodLenth).HasColumnName("BarcodLenth");
            this.Property(t => t.BarcodStartNumber).HasColumnName("BarcodStartNumber");
            this.Property(t => t.TrxComments).HasColumnName("TrxComments");
        }
    }
}
