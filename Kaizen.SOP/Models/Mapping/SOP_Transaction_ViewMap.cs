using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP_Transaction_ViewMap : EntityTypeConfiguration<SOP_Transaction_View>
    {
        public SOP_Transaction_ViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DocNumber, t.BatchID, t.TRXTypeID, t.CUSTNMBR, t.AddressCode, t.Expr1, t.AMT, t.DEX_ROW_ID, t.CurrencyCode, t.ISOCode, t.CurrencySymbol, t.DecimalDigit, t.DecimalSeparator, t.GroupSeparator, t.GroupSizes });

            // Properties
            this.Property(t => t.TrxTypeName)
                .HasMaxLength(15);

            this.Property(t => t.DocNumber)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.BatchID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(21);

            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.AddressCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.CUSTNAME)
                .HasMaxLength(100);

            this.Property(t => t.DocDescription)
                .HasMaxLength(50);

            this.Property(t => t.Expr1)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(21);

            this.Property(t => t.AMT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DEX_ROW_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CurrencyName)
                .HasMaxLength(50);

            this.Property(t => t.ISOCode)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.CurrencySymbol)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.DecimalSeparator)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GroupSeparator)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SOP_Transaction_View");
            this.Property(t => t.TrxTypeName).HasColumnName("TrxTypeName");
            this.Property(t => t.DocNumber).HasColumnName("DocNumber");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.CUSTNAME).HasColumnName("CUSTNAME");
            this.Property(t => t.DocDescription).HasColumnName("DocDescription");
            this.Property(t => t.Expr1).HasColumnName("Expr1");
            this.Property(t => t.AMT).HasColumnName("AMT");
            this.Property(t => t.Cost).HasColumnName("Cost");
            this.Property(t => t.TradeDiscount).HasColumnName("TradeDiscount");
            this.Property(t => t.Markdown).HasColumnName("Markdown");
            this.Property(t => t.Cash).HasColumnName("Cash");
            this.Property(t => t.Miscellaneous).HasColumnName("Miscellaneous");
            this.Property(t => t.CheckPay).HasColumnName("CheckPay");
            this.Property(t => t.CreditCard).HasColumnName("CreditCard");
            this.Property(t => t.TermsDiscountTaken).HasColumnName("TermsDiscountTaken");
            this.Property(t => t.DEX_ROW_ID).HasColumnName("DEX_ROW_ID");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.CurrencyName).HasColumnName("CurrencyName");
            this.Property(t => t.ISOCode).HasColumnName("ISOCode");
            this.Property(t => t.CurrencySymbol).HasColumnName("CurrencySymbol");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.DecimalSeparator).HasColumnName("DecimalSeparator");
            this.Property(t => t.GroupSeparator).HasColumnName("GroupSeparator");
            this.Property(t => t.GroupSizes).HasColumnName("GroupSizes");
        }
    }
}
