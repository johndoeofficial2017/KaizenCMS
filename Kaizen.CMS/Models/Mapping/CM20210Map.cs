using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM20210Map : EntityTypeConfiguration<CM20210>
    {
        public CM20210Map()
        {
            // Primary Key
            this.HasKey(t => t.TrxID);

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TRXTypeID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TrxTypeName)
                .HasMaxLength(300);

            this.Property(t => t.ContractID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ContractName)
                .HasMaxLength(20);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ClientName)
                .HasMaxLength(100);

            this.Property(t => t.BatchID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CIFNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.CIFName)
                .HasMaxLength(500);

            this.Property(t => t.AddressCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AddressName)
                .HasMaxLength(300);

            this.Property(t => t.CaseAddess)
                .HasMaxLength(500);

            this.Property(t => t.CaseStatusname)
                .HasMaxLength(50);

            this.Property(t => t.CaseStatusChildName)
                .HasMaxLength(50);

            this.Property(t => t.CaseStatusComment)
                .HasMaxLength(1000);

            this.Property(t => t.TxtField01)
                .HasMaxLength(50);

            this.Property(t => t.TxtField02)
                .HasMaxLength(50);

            this.Property(t => t.TxtField03)
                .HasMaxLength(50);

            this.Property(t => t.CaseAccountNo)
                .HasMaxLength(50);

            this.Property(t => t.InvoiceNumber)
                .HasMaxLength(50);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LastPaymentBy)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ContactNo)
                .HasMaxLength(500);

            this.Property(t => t.WebSite)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CountryID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CityID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Phone01)
                .HasMaxLength(300);

            this.Property(t => t.Phone02)
                .HasMaxLength(300);

            this.Property(t => t.Ext1)
                .HasMaxLength(5);

            this.Property(t => t.Ext2)
                .HasMaxLength(5);

            this.Property(t => t.MobileNo1)
                .HasMaxLength(100);

            this.Property(t => t.MobileNo2)
                .HasMaxLength(100);

            this.Property(t => t.POBox)
                .HasMaxLength(100);

            this.Property(t => t.Other01)
                .HasMaxLength(100);

            this.Property(t => t.Other02)
                .HasMaxLength(100);

            this.Property(t => t.Address1)
                .HasMaxLength(300);

            this.Property(t => t.Address2)
                .HasMaxLength(300);

            this.Property(t => t.Email01)
                .HasMaxLength(100);

            this.Property(t => t.Email02)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("CM20210");
            this.Property(t => t.TrxID).HasColumnName("TrxID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.TrxTypeName).HasColumnName("TrxTypeName");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.ContractName).HasColumnName("ContractName");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.CIFNumber).HasColumnName("CIFNumber");
            this.Property(t => t.CIFName).HasColumnName("CIFName");
            this.Property(t => t.IsJoint).HasColumnName("IsJoint");
            this.Property(t => t.IsHasHistory).HasColumnName("IsHasHistory");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
            this.Property(t => t.CaseAddess).HasColumnName("CaseAddess");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.CaseStatusChild).HasColumnName("CaseStatusChild");
            this.Property(t => t.CaseStatusname).HasColumnName("CaseStatusname");
            this.Property(t => t.CaseStatusChildName).HasColumnName("CaseStatusChildName");
            this.Property(t => t.CaseStatusComment).HasColumnName("CaseStatusComment");
            this.Property(t => t.ReminderDateTime).HasColumnName("ReminderDateTime");
            this.Property(t => t.PTPAMT).HasColumnName("PTPAMT");
            this.Property(t => t.CYCLEDAY).HasColumnName("CYCLEDAY");
            this.Property(t => t.GRADE).HasColumnName("GRADE");
            this.Property(t => t.BucketPrev).HasColumnName("BucketPrev");
            this.Property(t => t.BucketID).HasColumnName("BucketID");
            this.Property(t => t.TxtField01).HasColumnName("TxtField01");
            this.Property(t => t.TxtField02).HasColumnName("TxtField02");
            this.Property(t => t.TxtField03).HasColumnName("TxtField03");
            this.Property(t => t.OutstandingAMT).HasColumnName("OutstandingAMT");
            this.Property(t => t.OSAmount).HasColumnName("OSAmount");
            this.Property(t => t.FinanceCharge).HasColumnName("FinanceCharge");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.WriteOffAMT).HasColumnName("WriteOffAMT");
            this.Property(t => t.PrincipleAmount).HasColumnName("PrincipleAmount");
            this.Property(t => t.CreditLimit).HasColumnName("CreditLimit");
            this.Property(t => t.LastPaymentAMTUpload).HasColumnName("LastPaymentAMTUpload");
            this.Property(t => t.TotalLifeCollactedAMT).HasColumnName("TotalLifeCollactedAMT");
            this.Property(t => t.AMT01).HasColumnName("AMT01");
            this.Property(t => t.AMT02).HasColumnName("AMT02");
            this.Property(t => t.AMT03).HasColumnName("AMT03");
            this.Property(t => t.ContactTypeID).HasColumnName("ContactTypeID");
            this.Property(t => t.CaseAccountNo).HasColumnName("CaseAccountNo");
            this.Property(t => t.InvoiceNumber).HasColumnName("InvoiceNumber");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.OverDueDays).HasColumnName("OverDueDays");
            this.Property(t => t.OverDueDate).HasColumnName("OverDueDate");
            this.Property(t => t.FirstDisburementDate).HasColumnName("FirstDisburementDate");
            this.Property(t => t.LastPaymentDateUplod).HasColumnName("LastPaymentDateUplod");
            this.Property(t => t.ClosingDate).HasColumnName("ClosingDate");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.FirstLifeOverDueDate).HasColumnName("FirstLifeOverDueDate");
            this.Property(t => t.Date01).HasColumnName("Date01");
            this.Property(t => t.Date02).HasColumnName("Date02");
            this.Property(t => t.Date03).HasColumnName("Date03");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.AssignDate).HasColumnName("AssignDate");
            this.Property(t => t.LastPaymentDate).HasColumnName("LastPaymentDate");
            this.Property(t => t.LastCallactedAMT).HasColumnName("LastCallactedAMT");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
            this.Property(t => t.LastPaymentBy).HasColumnName("LastPaymentBy");
            this.Property(t => t.ContactNo).HasColumnName("ContactNo");
            this.Property(t => t.WebSite).HasColumnName("WebSite");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CityID).HasColumnName("CityID");
            this.Property(t => t.Phone01).HasColumnName("Phone01");
            this.Property(t => t.Phone02).HasColumnName("Phone02");
            this.Property(t => t.Ext1).HasColumnName("Ext1");
            this.Property(t => t.Ext2).HasColumnName("Ext2");
            this.Property(t => t.MobileNo1).HasColumnName("MobileNo1");
            this.Property(t => t.MobileNo2).HasColumnName("MobileNo2");
            this.Property(t => t.POBox).HasColumnName("POBox");
            this.Property(t => t.Other01).HasColumnName("Other01");
            this.Property(t => t.Other02).HasColumnName("Other02");
            this.Property(t => t.Address1).HasColumnName("Address1");
            this.Property(t => t.Address2).HasColumnName("Address2");
            this.Property(t => t.Email01).HasColumnName("Email01");
            this.Property(t => t.Email02).HasColumnName("Email02");
        }
    }
}
