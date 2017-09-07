using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00024_2317Map : EntityTypeConfiguration<CM00024_2317>
    {
        public CM00024_2317Map()
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

            this.Property(t => t.Lookup02)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Lookup02ValueName)
                .HasMaxLength(50);

            this.Property(t => t.TxtField01)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00024_2317");
            this.Property(t => t.StatusHistoryID).HasColumnName("StatusHistoryID");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.CaseStatusChild).HasColumnName("CaseStatusChild");
            this.Property(t => t.CaseStatusname).HasColumnName("CaseStatusname");
            this.Property(t => t.CaseStatusChildName).HasColumnName("CaseStatusChildName");
            this.Property(t => t.StatusScriptID).HasColumnName("StatusScriptID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ChangeStatusSourceID).HasColumnName("ChangeStatusSourceID");
            this.Property(t => t.ReminderDateTime).HasColumnName("ReminderDateTime");
            this.Property(t => t.CaseStatusComment).HasColumnName("CaseStatusComment");
            this.Property(t => t.PTPAMT).HasColumnName("PTPAMT");
            this.Property(t => t.Lookup01).HasColumnName("Lookup01");
            this.Property(t => t.Lookup01ValueName).HasColumnName("Lookup01ValueName");
            this.Property(t => t.Lookup02).HasColumnName("Lookup02");
            this.Property(t => t.Lookup02ValueName).HasColumnName("Lookup02ValueName");
            this.Property(t => t.Date01).HasColumnName("Date01");
            this.Property(t => t.TxtField01).HasColumnName("TxtField01");
        }
    }
}
