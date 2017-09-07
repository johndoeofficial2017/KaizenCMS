using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM30203Map : EntityTypeConfiguration<CM30203>
    {
        public CM30203Map()
        {
            // Primary Key
            this.HasKey(t => t.TrxID);

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.YearCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PeriodName)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.ContractID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ContractName)
                .HasMaxLength(50);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ClientName)
                .HasMaxLength(100);

            this.Property(t => t.NationaltyClient)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.NationltyCreditor)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.NationltyPartner)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.NationaltyIDCIF)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.NationalityName)
                .HasMaxLength(50);

            this.Property(t => t.EmployerName)
                .HasMaxLength(200);

            this.Property(t => t.CreditorID)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.CreditorName)
                .HasMaxLength(50);

            this.Property(t => t.PartnerID)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.PartnerName)
                .HasMaxLength(100);

            this.Property(t => t.LegalID)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.LegalName)
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

            this.Property(t => t.CPRCRNo)
                .HasMaxLength(50);

            this.Property(t => t.CIFName)
                .HasMaxLength(500);

            this.Property(t => t.PriorityName)
                .HasMaxLength(50);

            this.Property(t => t.CaseStatusname)
                .HasMaxLength(50);

            this.Property(t => t.CaseStatusChildName)
                .HasMaxLength(50);

            this.Property(t => t.CaseStatusComment)
                .HasMaxLength(1000);

            this.Property(t => t.LastCasStatusname)
                .HasMaxLength(50);

            this.Property(t => t.LastCasStatusChldNam)
                .HasMaxLength(50);

            this.Property(t => t.LastCaseStatusComment)
                .HasMaxLength(1000);

            this.Property(t => t.MidCasStatusnam)
                .HasMaxLength(50);

            this.Property(t => t.MidCasStatusChldName)
                .HasMaxLength(50);

            this.Property(t => t.MidCasStatusComment)
                .HasMaxLength(1000);

            this.Property(t => t.BucketPrevName)
                .HasMaxLength(50);

            this.Property(t => t.BucketName)
                .HasMaxLength(50);

            this.Property(t => t.Lookup01Name)
                .HasMaxLength(50);

            this.Property(t => t.Lookup02Name)
                .HasMaxLength(50);

            this.Property(t => t.ProductName)
                .HasMaxLength(50);

            this.Property(t => t.CaseAccountNo)
                .HasMaxLength(50);

            this.Property(t => t.InvoiceNumber)
                .HasMaxLength(50);

            this.Property(t => t.AgentID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AssignHistoryID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LastPaymentBy)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AddressCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseAddess)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("CM30203");
            this.Property(t => t.TrxID).HasColumnName("TrxID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.YearCode).HasColumnName("YearCode");
            this.Property(t => t.PERIODID).HasColumnName("PERIODID");
            this.Property(t => t.PeriodName).HasColumnName("PeriodName");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.ContractName).HasColumnName("ContractName");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.NationaltyClient).HasColumnName("NationaltyClient");
            this.Property(t => t.NationltyCreditor).HasColumnName("NationltyCreditor");
            this.Property(t => t.NationltyPartner).HasColumnName("NationltyPartner");
            this.Property(t => t.NationaltyIDCIF).HasColumnName("NationaltyIDCIF");
            this.Property(t => t.NationalityName).HasColumnName("NationalityName");
            this.Property(t => t.EmployerName).HasColumnName("EmployerName");
            this.Property(t => t.CreditorID).HasColumnName("CreditorID");
            this.Property(t => t.CreditorName).HasColumnName("CreditorName");
            this.Property(t => t.PartnerID).HasColumnName("PartnerID");
            this.Property(t => t.PartnerName).HasColumnName("PartnerName");
            this.Property(t => t.PartnrAssinDate).HasColumnName("PartnrAssinDate");
            this.Property(t => t.LegalID).HasColumnName("LegalID");
            this.Property(t => t.LegalName).HasColumnName("LegalName");
            this.Property(t => t.LegalAssignDate).HasColumnName("LegalAssignDate");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.CIFNumber).HasColumnName("CIFNumber");
            this.Property(t => t.CPRCRNo).HasColumnName("CPRCRNo");
            this.Property(t => t.CIFName).HasColumnName("CIFName");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.PriorityName).HasColumnName("PriorityName");
            this.Property(t => t.NPA).HasColumnName("NPA");
            this.Property(t => t.TaskComplated).HasColumnName("TaskComplated");
            this.Property(t => t.ContactTypeID).HasColumnName("ContactTypeID");
            this.Property(t => t.IsJoint).HasColumnName("IsJoint");
            this.Property(t => t.IsHasHistory).HasColumnName("IsHasHistory");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.CaseStatusChild).HasColumnName("CaseStatusChild");
            this.Property(t => t.CaseStatusname).HasColumnName("CaseStatusname");
            this.Property(t => t.CaseStatusChildName).HasColumnName("CaseStatusChildName");
            this.Property(t => t.CaseStatusComment).HasColumnName("CaseStatusComment");
            this.Property(t => t.ReminderDateTime).HasColumnName("ReminderDateTime");
            this.Property(t => t.PTPAMT).HasColumnName("PTPAMT");
            this.Property(t => t.PTPCount).HasColumnName("PTPCount");
            this.Property(t => t.PTPBroken).HasColumnName("PTPBroken");
            this.Property(t => t.PTPkept).HasColumnName("PTPkept");
            this.Property(t => t.LastCaseStatusID).HasColumnName("LastCaseStatusID");
            this.Property(t => t.LastCasStatusChldID).HasColumnName("LastCasStatusChldID");
            this.Property(t => t.LastCasStatusname).HasColumnName("LastCasStatusname");
            this.Property(t => t.LastCasStatusChldNam).HasColumnName("LastCasStatusChldNam");
            this.Property(t => t.LastCaseStatusComment).HasColumnName("LastCaseStatusComment");
            this.Property(t => t.MidCasStatusID).HasColumnName("MidCasStatusID");
            this.Property(t => t.MidCasStatusChld).HasColumnName("MidCasStatusChld");
            this.Property(t => t.MidCasStatusnam).HasColumnName("MidCasStatusnam");
            this.Property(t => t.MidCasStatusChldName).HasColumnName("MidCasStatusChldName");
            this.Property(t => t.MidCasStatusComment).HasColumnName("MidCasStatusComment");
            this.Property(t => t.CYCLEDAY).HasColumnName("CYCLEDAY");
            this.Property(t => t.BucketPrev).HasColumnName("BucketPrev");
            this.Property(t => t.BucketPrevName).HasColumnName("BucketPrevName");
            this.Property(t => t.BucketID).HasColumnName("BucketID");
            this.Property(t => t.BucketName).HasColumnName("BucketName");
            this.Property(t => t.Lookup01).HasColumnName("Lookup01");
            this.Property(t => t.Lookup01Name).HasColumnName("Lookup01Name");
            this.Property(t => t.Lookup02).HasColumnName("Lookup02");
            this.Property(t => t.Lookup02Name).HasColumnName("Lookup02Name");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.TxtField01).HasColumnName("TxtField01");
            this.Property(t => t.TxtField02).HasColumnName("TxtField02");
            this.Property(t => t.TxtField03).HasColumnName("TxtField03");
            this.Property(t => t.TxtField04).HasColumnName("TxtField04");
            this.Property(t => t.TxtField05).HasColumnName("TxtField05");
            this.Property(t => t.TxtField06).HasColumnName("TxtField06");
            this.Property(t => t.TxtField07).HasColumnName("TxtField07");
            this.Property(t => t.CheckBoxField02).HasColumnName("CheckBoxField02");
            this.Property(t => t.CheckBoxField01).HasColumnName("CheckBoxField01");
            this.Property(t => t.AMT01).HasColumnName("AMT01");
            this.Property(t => t.AMT02).HasColumnName("AMT02");
            this.Property(t => t.AMT03).HasColumnName("AMT03");
            this.Property(t => t.AMT04).HasColumnName("AMT04");
            this.Property(t => t.AMT05).HasColumnName("AMT05");
            this.Property(t => t.AMT06).HasColumnName("AMT06");
            this.Property(t => t.AMT07).HasColumnName("AMT07");
            this.Property(t => t.AMT08).HasColumnName("AMT08");
            this.Property(t => t.AMT09).HasColumnName("AMT09");
            this.Property(t => t.AMT10).HasColumnName("AMT10");
            this.Property(t => t.Date01).HasColumnName("Date01");
            this.Property(t => t.Date02).HasColumnName("Date02");
            this.Property(t => t.Date03).HasColumnName("Date03");
            this.Property(t => t.Date04).HasColumnName("Date04");
            this.Property(t => t.PrincipleAmount).HasColumnName("PrincipleAmount");
            this.Property(t => t.MaturityAmount).HasColumnName("MaturityAmount");
            this.Property(t => t.OutstandingAMT).HasColumnName("OutstandingAMT");
            this.Property(t => t.OutStandingToday).HasColumnName("OutStandingToday");
            this.Property(t => t.InstallmentAmount).HasColumnName("InstallmentAmount");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.FinanceCharge).HasColumnName("FinanceCharge");
            this.Property(t => t.WriteOffAMT).HasColumnName("WriteOffAMT");
            this.Property(t => t.CreditLimit).HasColumnName("CreditLimit");
            this.Property(t => t.LastPaymentAMTUpload).HasColumnName("LastPaymentAMTUpload");
            this.Property(t => t.TotalLifeCollactedAMT).HasColumnName("TotalLifeCollactedAMT");
            this.Property(t => t.CaseAccountNo).HasColumnName("CaseAccountNo");
            this.Property(t => t.InvoiceNumber).HasColumnName("InvoiceNumber");
            this.Property(t => t.OverDueDays).HasColumnName("OverDueDays");
            this.Property(t => t.FirstDisburementDate).HasColumnName("FirstDisburementDate");
            this.Property(t => t.MATURITY_DATE).HasColumnName("MATURITY_DATE");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.OverDueDate).HasColumnName("OverDueDate");
            this.Property(t => t.LastPaymentDateUpload).HasColumnName("LastPaymentDateUpload");
            this.Property(t => t.ClosingDate).HasColumnName("ClosingDate");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.FirstLifeOverDueDate).HasColumnName("FirstLifeOverDueDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.AssignDate).HasColumnName("AssignDate");
            this.Property(t => t.AssignHistoryID).HasColumnName("AssignHistoryID");
            this.Property(t => t.LastPaymentDate).HasColumnName("LastPaymentDate");
            this.Property(t => t.LastCallactedAMT).HasColumnName("LastCallactedAMT");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
            this.Property(t => t.CommissionRate).HasColumnName("CommissionRate");
            this.Property(t => t.LastPaymentBy).HasColumnName("LastPaymentBy");
            this.Property(t => t.MonthlySalary).HasColumnName("MonthlySalary");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.CaseAddess).HasColumnName("CaseAddess");
        }
    }
}
