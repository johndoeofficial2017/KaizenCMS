using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10100Map : EntityTypeConfiguration<SOP10100>
    {
        public SOP10100Map()
        {
            // Primary Key
            this.HasKey(t => t.SOPNUMBE);

            // Properties
            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SOPNUMBEORG)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BatchID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ProjectID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PaymentTermID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTNAME)
                .HasMaxLength(1000);

            this.Property(t => t.BillAddressTypeCode)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.BillAddressName)
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

            this.Property(t => t.CustomerPoNumber)
                .HasMaxLength(15);

            this.Property(t => t.ShippingID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SiteID)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ExchangeTableID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Territory)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SalePersonID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TrxComments)
                .HasMaxLength(1000);

            this.Property(t => t.VendorID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CountryID)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP10100");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.SOPNUMBEORG).HasColumnName("SOPNUMBEORG");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.ProjectID).HasColumnName("ProjectID");
            this.Property(t => t.PaymentTermID).HasColumnName("PaymentTermID");
            this.Property(t => t.DOCDATE).HasColumnName("DOCDATE");
            this.Property(t => t.DOCAMNT).HasColumnName("DOCAMNT");
            this.Property(t => t.DOCAMNTCollected).HasColumnName("DOCAMNTCollected");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.CUSTNAME).HasColumnName("CUSTNAME");
            this.Property(t => t.BillAddressTypeCode).HasColumnName("BillAddressTypeCode");
            this.Property(t => t.BillAddressName).HasColumnName("BillAddressName");
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
            this.Property(t => t.CustomerPoNumber).HasColumnName("CustomerPoNumber");
            this.Property(t => t.PriceLevelCode).HasColumnName("PriceLevelCode");
            this.Property(t => t.ShippingID).HasColumnName("ShippingID");
            this.Property(t => t.ReqShipDate).HasColumnName("ReqShipDate");
            this.Property(t => t.ShipDate).HasColumnName("ShipDate");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.BinTrack).HasColumnName("BinTrack");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.ExchangeTableID).HasColumnName("ExchangeTableID");
            this.Property(t => t.ExchangeRateID).HasColumnName("ExchangeRateID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.Territory).HasColumnName("Territory");
            this.Property(t => t.SalePersonID).HasColumnName("SalePersonID");
            this.Property(t => t.TaxAMT).HasColumnName("TaxAMT");
            this.Property(t => t.Markdown).HasColumnName("Markdown");
            this.Property(t => t.Freight).HasColumnName("Freight");
            this.Property(t => t.Miscellaneous).HasColumnName("Miscellaneous");
            this.Property(t => t.TradeDiscount).HasColumnName("TradeDiscount");
            this.Property(t => t.TrxComments).HasColumnName("TrxComments");
            this.Property(t => t.VendorID).HasColumnName("VendorID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");

            // Relationships
            this.HasOptional(t => t.SOP00004)
                .WithMany(t => t.SOP10100)
                .HasForeignKey(d => d.BatchID);
            this.HasOptional(t => t.SOP00015)
                .WithMany(t => t.SOP10100)
                .HasForeignKey(d => d.PaymentTermID);
            this.HasRequired(t => t.SOP00100)
                .WithMany(t => t.SOP10100)
                .HasForeignKey(d => d.CUSTNMBR);

        }
    }
}
