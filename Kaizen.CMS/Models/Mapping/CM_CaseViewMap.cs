using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_CaseViewMap : EntityTypeConfiguration<CM_CaseView>
    {
        public CM_CaseViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ChildCaseStatusName, t.CaseRef, t.ContractID, t.ClientID, t.CurrencyCode, t.DecimalDigit, t.DebtorID, t.CaseStatusID, t.OSAmount, t.FinanceCharge, t.ClaimAmount, t.PrincipleAmount, t.CreatedDate, t.AgentID, t.AssignDate });

            // Properties
            this.Property(t => t.CaseStatusname)
                .HasMaxLength(50);

            this.Property(t => t.ChildCaseStatusName)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ContractID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TrxTypeName)
                .HasMaxLength(30);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.CaseStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CaseStatusComment)
                .HasMaxLength(1000);

            this.Property(t => t.AddressCode)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.CaseAddess)
                .HasMaxLength(500);

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

            // Table & Column Mappings
            this.ToTable("CM_CaseView");
            this.Property(t => t.CaseStatusname).HasColumnName("CaseStatusname");
            this.Property(t => t.ChildCaseStatusName).HasColumnName("ChildCaseStatusName");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.TrxTypeName).HasColumnName("TrxTypeName");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.TaskProgress).HasColumnName("TaskProgress");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.CaseStatusChild).HasColumnName("CaseStatusChild");
            this.Property(t => t.CaseStatusComment).HasColumnName("CaseStatusComment");
            this.Property(t => t.ReminderDateTime).HasColumnName("ReminderDateTime");
            this.Property(t => t.PTPAMT).HasColumnName("PTPAMT");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.CaseAddess).HasColumnName("CaseAddess");
            this.Property(t => t.ContactTypeID).HasColumnName("ContactTypeID");
            this.Property(t => t.CaseAccountNo).HasColumnName("CaseAccountNo");
            this.Property(t => t.InvoiceNumber).HasColumnName("InvoiceNumber");
            this.Property(t => t.ClosingDate).HasColumnName("ClosingDate");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.OSAmount).HasColumnName("OSAmount");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.FinanceCharge).HasColumnName("FinanceCharge");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.PrincipleAmount).HasColumnName("PrincipleAmount");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.AssignDate).HasColumnName("AssignDate");
            this.Property(t => t.LastPaymentDate).HasColumnName("LastPaymentDate");
            this.Property(t => t.LastCallactedAMT).HasColumnName("LastCallactedAMT");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
            this.Property(t => t.LastPaymentBy).HasColumnName("LastPaymentBy");
        }
    }
}
