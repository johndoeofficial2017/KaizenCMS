using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00107Map : EntityTypeConfiguration<CM00107>
    {
        public CM00107Map()
        {
            // Primary Key
            this.HasKey(t => new { t.YearCode, t.AgentID, t.PERIODID, t.TargetID });

            // Properties
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

            this.Property(t => t.TargetID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PeriodName)
                .IsRequired()
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("CM00107");
            this.Property(t => t.YearCode).HasColumnName("YearCode");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.PERIODID).HasColumnName("PERIODID");
            this.Property(t => t.TargetID).HasColumnName("TargetID");
            this.Property(t => t.PeriodName).HasColumnName("PeriodName");
            this.Property(t => t.IsClosed).HasColumnName("IsClosed");
            this.Property(t => t.IsPercentTarget).HasColumnName("IsPercentTarget");
            this.Property(t => t.TargetTypeID).HasColumnName("TargetTypeID");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.CaseCount).HasColumnName("CaseCount");
            this.Property(t => t.DebtorCount).HasColumnName("DebtorCount");
            this.Property(t => t.TargetClaimAmount).HasColumnName("TargetClaimAmount");
            this.Property(t => t.TargetCaseCount).HasColumnName("TargetCaseCount");
            this.Property(t => t.TargetDebtorCount).HasColumnName("TargetDebtorCount");
            this.Property(t => t.CollectedAmount).HasColumnName("CollectedAmount");
            this.Property(t => t.CollectedCaseCount).HasColumnName("CollectedCaseCount");
            this.Property(t => t.CollectedDebtorCount).HasColumnName("CollectedDebtorCount");

            // Relationships
            this.HasRequired(t => t.CM00109)
                .WithMany(t => t.CM00107)
                .HasForeignKey(d => new { d.TargetID, d.AgentID });
            this.HasRequired(t => t.CM10110)
                .WithMany(t => t.CM00107)
                .HasForeignKey(d => new { d.YearCode, d.AgentID, d.PERIODID });

        }
    }
}
