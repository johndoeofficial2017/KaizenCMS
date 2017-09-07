using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10506Map : EntityTypeConfiguration<SOP10506>
    {
        public SOP10506Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ItemLineIndex, t.SOPNUMBE });

            // Properties
            this.Property(t => t.ItemLineIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ShipAddressTypeCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ShipAddressName)
                .HasMaxLength(50);

            this.Property(t => t.Pnone01)
                .HasMaxLength(50);

            this.Property(t => t.Pnone02)
                .HasMaxLength(50);

            this.Property(t => t.MobileNo1)
                .HasMaxLength(50);

            this.Property(t => t.MobileNo2)
                .HasMaxLength(50);

            this.Property(t => t.POBox)
                .HasMaxLength(50);

            this.Property(t => t.Other01)
                .HasMaxLength(50);

            this.Property(t => t.Other02)
                .HasMaxLength(50);

            this.Property(t => t.Address1)
                .HasMaxLength(50);

            this.Property(t => t.Email01)
                .HasMaxLength(50);

            this.Property(t => t.Email02)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP10506");
            this.Property(t => t.ItemLineIndex).HasColumnName("ItemLineIndex");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.ShipAddressTypeCode).HasColumnName("ShipAddressTypeCode");
            this.Property(t => t.ShipAddressName).HasColumnName("ShipAddressName");
            this.Property(t => t.Pnone01).HasColumnName("Pnone01");
            this.Property(t => t.Pnone02).HasColumnName("Pnone02");
            this.Property(t => t.MobileNo1).HasColumnName("MobileNo1");
            this.Property(t => t.MobileNo2).HasColumnName("MobileNo2");
            this.Property(t => t.POBox).HasColumnName("POBox");
            this.Property(t => t.Other01).HasColumnName("Other01");
            this.Property(t => t.Other02).HasColumnName("Other02");
            this.Property(t => t.Address1).HasColumnName("Address1");
            this.Property(t => t.Email01).HasColumnName("Email01");
            this.Property(t => t.Email02).HasColumnName("Email02");

            // Relationships
            this.HasRequired(t => t.SOP10501)
                .WithOptional(t => t.SOP10506);

        }
    }
}
