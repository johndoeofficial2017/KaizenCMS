using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00216Map : EntityTypeConfiguration<CM00216>
    {
        public CM00216Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ContractID, t.YearCode, t.AgentID, t.PERIODID });

            // Properties
            this.Property(t => t.ContractID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.YearCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PERIODID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ContractName)
                .HasMaxLength(20);

            this.Property(t => t.PeriodName)
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("CM00216");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.YearCode).HasColumnName("YearCode");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.PERIODID).HasColumnName("PERIODID");
            this.Property(t => t.ContractName).HasColumnName("ContractName");
            this.Property(t => t.PeriodName).HasColumnName("PeriodName");
            this.Property(t => t.TargetAmount).HasColumnName("TargetAmount");
            this.Property(t => t.OutStandingAMT).HasColumnName("OutStandingAMT");
            this.Property(t => t.CollectedAmount).HasColumnName("CollectedAmount");
            this.Property(t => t.DebtorCount).HasColumnName("DebtorCount");
            this.Property(t => t.CaseCount).HasColumnName("CaseCount");
            this.Property(t => t.OutStandingAMTPost).HasColumnName("OutStandingAMTPost");
            this.Property(t => t.CollectedAmountPost).HasColumnName("CollectedAmountPost");
            this.Property(t => t.DebtorCountPost).HasColumnName("DebtorCountPost");
            this.Property(t => t.CaseCountPost).HasColumnName("CaseCountPost");

        }
    }
}
