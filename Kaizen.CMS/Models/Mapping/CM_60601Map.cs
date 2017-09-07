using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_60601Map : EntityTypeConfiguration<CM_60601>
    {
        public CM_60601Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ClientID, t.ContractID, t.AgentID, t.PERIODID, t.PeriodName, t.BucketID });

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

            this.Property(t => t.PeriodName)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.BucketID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM_60601");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.PERIODID).HasColumnName("PERIODID");
            this.Property(t => t.PeriodName).HasColumnName("PeriodName");
            this.Property(t => t.BucketID).HasColumnName("BucketID");
            this.Property(t => t.OutStandingAMT).HasColumnName("OutStandingAMT");
            this.Property(t => t.DebtorCount).HasColumnName("DebtorCount");
            this.Property(t => t.CaseCount).HasColumnName("CaseCount");
        }
    }
}
