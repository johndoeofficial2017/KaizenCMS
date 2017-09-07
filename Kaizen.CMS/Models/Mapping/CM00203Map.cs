using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00203Map : EntityTypeConfiguration<CM00203>
    {
        public CM00203Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CaseRef, t.TRXTypeID });

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.TRXTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ContractID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ContractName)
                .IsRequired()
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
                .HasMaxLength(100);

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

            this.Property(t => t.LastCasStatusComment)
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

            this.Property(t => t.LastPaymentBy)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AddressCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseAddess)
                .HasMaxLength(500);

            this.Property(t => t.Phone01)
                .HasMaxLength(100);

            this.Property(t => t.Phone02)
                .HasMaxLength(100);

            this.Property(t => t.Phone03)
                .HasMaxLength(100);

            this.Property(t => t.Phone04)
                .HasMaxLength(100);

            this.Property(t => t.MobileNo1)
                .HasMaxLength(100);

            this.Property(t => t.MobileNo2)
                .HasMaxLength(100);

            this.Property(t => t.MobileNo3)
                .HasMaxLength(100);

            this.Property(t => t.MobileNo4)
                .HasMaxLength(100);

            this.Property(t => t.POBox)
                .HasMaxLength(50);

            this.Property(t => t.Other01)
                .HasMaxLength(100);

            this.Property(t => t.Other02)
                .HasMaxLength(100);

            this.Property(t => t.Other03)
                .HasMaxLength(100);

            this.Property(t => t.Other04)
                .HasMaxLength(100);

            this.Property(t => t.Address1)
                .HasMaxLength(100);

            this.Property(t => t.Address2)
                .HasMaxLength(100);

            this.Property(t => t.Address3)
                .HasMaxLength(100);

            this.Property(t => t.Address4)
                .HasMaxLength(100);

            this.Property(t => t.Email01)
                .HasMaxLength(50);

            this.Property(t => t.Email02)
                .HasMaxLength(50);

            this.Property(t => t.Email03)
                .HasMaxLength(50);

            this.Property(t => t.Email04)
                .HasMaxLength(50);

            this.Property(t => t.FlatNo)
                .HasMaxLength(50);

            this.Property(t => t.BuildingNo)
                .HasMaxLength(50);

            this.Property(t => t.RoadName)
                .HasMaxLength(50);

            this.Property(t => t.RoadNo)
                .HasMaxLength(50);

            this.Property(t => t.BlockNo)
                .HasMaxLength(50);

            this.Property(t => t.BlockName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00203");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.IsHold).HasColumnName("IsHold");
            this.Property(t => t.PstDueCountOpen).HasColumnName("PstDueCountOpen");
            this.Property(t => t.PstDueCountClient).HasColumnName("PstDueCountClient");
            this.Property(t => t.PstDueCountHstry).HasColumnName("PstDueCountHstry");
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
            this.Property(t => t.LastCasStatusComment).HasColumnName("LastCasStatusComment");
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
            this.Property(t => t.CheckBoxField03).HasColumnName("CheckBoxField03");
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
            this.Property(t => t.LastPaymentDateUplod).HasColumnName("LastPaymentDateUplod");
            this.Property(t => t.ClosingDate).HasColumnName("ClosingDate");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.FirstLifeOverDueDate).HasColumnName("FirstLifeOverDueDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.AssignDate).HasColumnName("AssignDate");
            this.Property(t => t.LastPaymentDate).HasColumnName("LastPaymentDate");
            this.Property(t => t.LastCallactedAMT).HasColumnName("LastCallactedAMT");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
            this.Property(t => t.CommissionRate).HasColumnName("CommissionRate");
            this.Property(t => t.LastPaymentBy).HasColumnName("LastPaymentBy");
            this.Property(t => t.MonthlySalary).HasColumnName("MonthlySalary");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.CaseAddess).HasColumnName("CaseAddess");
            this.Property(t => t.Phone01).HasColumnName("Phone01");
            this.Property(t => t.Phone02).HasColumnName("Phone02");
            this.Property(t => t.Phone03).HasColumnName("Phone03");
            this.Property(t => t.Phone04).HasColumnName("Phone04");
            this.Property(t => t.MobileNo1).HasColumnName("MobileNo1");
            this.Property(t => t.MobileNo2).HasColumnName("MobileNo2");
            this.Property(t => t.MobileNo3).HasColumnName("MobileNo3");
            this.Property(t => t.MobileNo4).HasColumnName("MobileNo4");
            this.Property(t => t.POBox).HasColumnName("POBox");
            this.Property(t => t.Other01).HasColumnName("Other01");
            this.Property(t => t.Other02).HasColumnName("Other02");
            this.Property(t => t.Other03).HasColumnName("Other03");
            this.Property(t => t.Other04).HasColumnName("Other04");
            this.Property(t => t.Address1).HasColumnName("Address1");
            this.Property(t => t.Address2).HasColumnName("Address2");
            this.Property(t => t.Address3).HasColumnName("Address3");
            this.Property(t => t.Address4).HasColumnName("Address4");
            this.Property(t => t.Email01).HasColumnName("Email01");
            this.Property(t => t.Email02).HasColumnName("Email02");
            this.Property(t => t.Email03).HasColumnName("Email03");
            this.Property(t => t.Email04).HasColumnName("Email04");
            this.Property(t => t.FlatNo).HasColumnName("FlatNo");
            this.Property(t => t.BuildingNo).HasColumnName("BuildingNo");
            this.Property(t => t.RoadName).HasColumnName("RoadName");
            this.Property(t => t.RoadNo).HasColumnName("RoadNo");
            this.Property(t => t.BlockNo).HasColumnName("BlockNo");
            this.Property(t => t.BlockName).HasColumnName("BlockName");

            // Relationships
            this.HasOptional(t => t.CM00015)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.BucketID);
            this.HasOptional(t => t.CM00016)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.Lookup01);
            this.HasOptional(t => t.CM00017)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.Lookup02);
            this.HasRequired(t => t.CM00029)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.TRXTypeID);
            this.HasOptional(t => t.CM00055)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.ProductID);
            this.HasRequired(t => t.CM00100)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.CIFNumber);
            this.HasOptional(t => t.CM00105)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.AgentID);
            this.HasOptional(t => t.CM00130)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.PartnerID);
            this.HasOptional(t => t.CM00140)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.LegalID);
            this.HasOptional(t => t.CM00170)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.CreditorID);
            this.HasRequired(t => t.CM00700)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.CaseStatusID);
            this.HasRequired(t => t.CM10100)
                .WithMany(t => t.CM00203)
                .HasForeignKey(d => d.BatchID);

        }
    }
}
