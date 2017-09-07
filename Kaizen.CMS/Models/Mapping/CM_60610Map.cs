using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_60610Map : EntityTypeConfiguration<CM_60610>
    {
        public CM_60610Map()
        {
            // Primary Key
            this.HasKey(t => new { t.AgentID, t.CurrentPeriod });

            // Properties
            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CurrentPeriod)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM_60610");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            //this.Property(t => t.TargetAmount).HasColumnName("TargetAmount");
            //this.Property(t => t.OutStandingAMT).HasColumnName("OutStandingAMT");
            this.Property(t => t.CollectedAmount).HasColumnName("CollectedAmount");
            this.Property(t => t.DebtorCount).HasColumnName("DebtorCount");
            this.Property(t => t.CaseCount).HasColumnName("CaseCount");
            this.Property(t => t.CurrentPeriod).HasColumnName("CurrentPeriod");
            this.Property(t => t.PreviousValue).HasColumnName("PreviousValue");
            this.Property(t => t.PreviousTargetAmount).HasColumnName("PreviousTargetAmount");
            this.Property(t => t.PreviousCollectedAmount).HasColumnName("PreviousCollectedAmount");
            this.Property(t => t.PreviousDebtorCount).HasColumnName("PreviousDebtorCount");
            this.Property(t => t.PreviousCaseCount).HasColumnName("PreviousCaseCount");
        }
    }
}
