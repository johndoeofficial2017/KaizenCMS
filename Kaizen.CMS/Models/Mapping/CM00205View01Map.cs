using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00205View01Map : EntityTypeConfiguration<CM00205View01>
    {
        public CM00205View01Map()
        {
            // Primary Key
            this.HasKey(t => t.CaseStatusID);

            // Properties
            this.Property(t => t.StatusMonth)
                .HasMaxLength(9);

            this.Property(t => t.CaseStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CaseStatusname)
                .HasMaxLength(50);

            this.Property(t => t.CaseStatusChildName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00205View01");
            this.Property(t => t.StatusYear).HasColumnName("StatusYear");
            this.Property(t => t.StatusMonth).HasColumnName("StatusMonth");
            this.Property(t => t.StatusDay).HasColumnName("StatusDay");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.CaseStatusname).HasColumnName("CaseStatusname");
            this.Property(t => t.CaseStatusChild).HasColumnName("CaseStatusChild");
            this.Property(t => t.CaseStatusChildName).HasColumnName("CaseStatusChildName");
            this.Property(t => t.PTPAMT).HasColumnName("PTPAMT");
            this.Property(t => t.StatusHistoryID).HasColumnName("StatusHistoryID");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.OutstandingAMT).HasColumnName("OutstandingAMT");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
        }
    }
}
