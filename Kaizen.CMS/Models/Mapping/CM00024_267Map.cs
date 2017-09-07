using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00024_267Map : EntityTypeConfiguration<CM00024_267>
    {
        public CM00024_267Map()
        {
            // Primary Key
            this.HasKey(t => t.StatusHistoryID);

            // Properties
            this.Property(t => t.CaseStatusname)
                .HasMaxLength(50);

            this.Property(t => t.CaseStatusChildName)
                .HasMaxLength(50);

            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseStatusComment)
                .HasMaxLength(1000);

            this.Property(t => t.Lookup01)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Lookup01ValueName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00024_267");
            this.Property(t => t.StatusHistoryID).HasColumnName("StatusHistoryID");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.CaseStatusChild).HasColumnName("CaseStatusChild");
            this.Property(t => t.CaseStatusname).HasColumnName("CaseStatusname");
            this.Property(t => t.CaseStatusChildName).HasColumnName("CaseStatusChildName");
            this.Property(t => t.StatusScriptID).HasColumnName("StatusScriptID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.OutstandingAMT).HasColumnName("OutstandingAMT");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ChangeStatusSourceID).HasColumnName("ChangeStatusSourceID");
            this.Property(t => t.ReminderDateTime).HasColumnName("ReminderDateTime");
            this.Property(t => t.CaseStatusComment).HasColumnName("CaseStatusComment");
            this.Property(t => t.PTPAMT).HasColumnName("PTPAMT");
            this.Property(t => t.Lookup01).HasColumnName("Lookup01");
            this.Property(t => t.Lookup01ValueName).HasColumnName("Lookup01ValueName");
            this.Property(t => t.Date01).HasColumnName("Date01");
            this.Property(t => t.Date02).HasColumnName("Date02");
            this.Property(t => t.Date03).HasColumnName("Date03");
            this.Property(t => t.Date04).HasColumnName("Date04");
            this.Property(t => t.Date05).HasColumnName("Date05");
            this.Property(t => t.Date06).HasColumnName("Date06");
            this.Property(t => t.Date07).HasColumnName("Date07");
            this.Property(t => t.Date08).HasColumnName("Date08");
            this.Property(t => t.Date09).HasColumnName("Date09");
            this.Property(t => t.Date10).HasColumnName("Date10");
        }
    }
}
