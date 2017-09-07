using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM10701Map : EntityTypeConfiguration<CM10701>
    {
        public CM10701Map()
        {
            // Primary Key
            this.HasKey(t => t.StatusHistoryID);

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

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

            this.Property(t => t.AgentID)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM10701");
            this.Property(t => t.StatusHistoryID).HasColumnName("StatusHistoryID");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.StatusActionTypeID).HasColumnName("StatusActionTypeID");
            this.Property(t => t.ChangeStatusSourceID).HasColumnName("ChangeStatusSourceID");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.CaseStatusChild).HasColumnName("CaseStatusChild");
            this.Property(t => t.CaseStatusname).HasColumnName("CaseStatusname");
            this.Property(t => t.CaseStatusChildName).HasColumnName("CaseStatusChildName");
            this.Property(t => t.CaseStatusComment).HasColumnName("CaseStatusComment");
            this.Property(t => t.ReminderDateTime).HasColumnName("ReminderDateTime");
            this.Property(t => t.PTPAMT).HasColumnName("PTPAMT");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.OutstandingAMT).HasColumnName("OutstandingAMT");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
            this.Property(t => t.LastCaseStatusID).HasColumnName("LastCaseStatusID");
            this.Property(t => t.LastCasStatusChldID).HasColumnName("LastCasStatusChldID");
            this.Property(t => t.LastCasStatusname).HasColumnName("LastCasStatusname");
            this.Property(t => t.LastCasStatusChldNam).HasColumnName("LastCasStatusChldNam");
            this.Property(t => t.LastCasStatusComment).HasColumnName("LastCasStatusComment");
            this.Property(t => t.StatusScriptID).HasColumnName("StatusScriptID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DecimalDigit).HasColumnName("DecimalDigit");
            this.Property(t => t.IsMultiply).HasColumnName("IsMultiply");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");

            // Relationships
            this.HasRequired(t => t.CM00027)
                .WithMany(t => t.CM10701)
                .HasForeignKey(d => d.ChangeStatusSourceID);
            this.HasRequired(t => t.CM00203)
                .WithMany(t => t.CM10701)
                .HasForeignKey(d => new { d.CaseRef, d.TRXTypeID });

        }
    }
}
