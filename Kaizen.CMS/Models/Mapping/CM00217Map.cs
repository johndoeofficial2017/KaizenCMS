using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00217Map : EntityTypeConfiguration<CM00217>
    {
        public CM00217Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ClientID, t.ContractID, t.AgentID, t.PERIODID, t.BucketID });

            // Properties
            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ContractID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PERIODID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BucketID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ClientName)
                .HasMaxLength(100);

            this.Property(t => t.ContractName)
                .HasMaxLength(20);

            this.Property(t => t.BucketName)
                .HasMaxLength(50);

            this.Property(t => t.PeriodName)
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("CM00217");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.PERIODID).HasColumnName("PERIODID");
            this.Property(t => t.BucketID).HasColumnName("BucketID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.ContractName).HasColumnName("ContractName");
            this.Property(t => t.BucketName).HasColumnName("BucketName");
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
