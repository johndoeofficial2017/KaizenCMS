using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM20205Map : EntityTypeConfiguration<CM20205>
    {
        public CM20205Map()
        {
            // Primary Key
            this.HasKey(t => t.StatusHistoryID);

            // Properties
            this.Property(t => t.StatusHistoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

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

            // Table & Column Mappings
            this.ToTable("CM20205");
            this.Property(t => t.StatusHistoryID).HasColumnName("StatusHistoryID");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.StatusActionTypeID).HasColumnName("StatusActionTypeID");
            this.Property(t => t.ChangeStatusSourceID).HasColumnName("ChangeStatusSourceID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.CaseStatusChild).HasColumnName("CaseStatusChild");
            this.Property(t => t.CaseStatusname).HasColumnName("CaseStatusname");
            this.Property(t => t.CaseStatusChildName).HasColumnName("CaseStatusChildName");
            this.Property(t => t.CaseStatusComment).HasColumnName("CaseStatusComment");
            this.Property(t => t.ReminderDateTime).HasColumnName("ReminderDateTime");
            this.Property(t => t.PTPAMT).HasColumnName("PTPAMT");
        }
    }
}
