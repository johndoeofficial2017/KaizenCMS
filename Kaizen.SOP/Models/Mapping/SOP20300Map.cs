using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP20300Map : EntityTypeConfiguration<SOP20300>
    {
        public SOP20300Map()
        {
            // Primary Key
            this.HasKey(t => t.SOPNUMBE);

            // Properties
            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SiteID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SubBinID)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTNAME)
                .HasMaxLength(1000);

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

            this.Property(t => t.TrxComments)
                .HasMaxLength(1000);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP20300");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.SubBinID).HasColumnName("SubBinID");
            this.Property(t => t.BinID).HasColumnName("BinID");
            this.Property(t => t.DOCDATE).HasColumnName("DOCDATE");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.DOCAMNT).HasColumnName("DOCAMNT");
            this.Property(t => t.CollectedAMT).HasColumnName("CollectedAMT");
            this.Property(t => t.PartialAMT).HasColumnName("PartialAMT");
            this.Property(t => t.TotalCash).HasColumnName("TotalCash");
            this.Property(t => t.TotalCheck).HasColumnName("TotalCheck");
            this.Property(t => t.TotalCreditCard).HasColumnName("TotalCreditCard");
            this.Property(t => t.TotalVoucher).HasColumnName("TotalVoucher");
            this.Property(t => t.TaxAMT).HasColumnName("TaxAMT");
            this.Property(t => t.Freight).HasColumnName("Freight");
            this.Property(t => t.Markdown).HasColumnName("Markdown");
            this.Property(t => t.Miscellaneous).HasColumnName("Miscellaneous");
            this.Property(t => t.TradeDiscount).HasColumnName("TradeDiscount");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.CUSTNAME).HasColumnName("CUSTNAME");
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
            this.Property(t => t.TrxComments).HasColumnName("TrxComments");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
