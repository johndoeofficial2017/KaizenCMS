using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00007Map : EntityTypeConfiguration<CM00007>
    {
        public CM00007Map()
        {
            // Primary Key
            this.HasKey(t => t.PERIODID);

            // Properties
            this.Property(t => t.YearCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PeriodName)
                .IsRequired()
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("CM00007");
            this.Property(t => t.PERIODID).HasColumnName("PERIODID");
            this.Property(t => t.CalendarID).HasColumnName("CalendarID");
            this.Property(t => t.YearCode).HasColumnName("YearCode");
            this.Property(t => t.PeriodName).HasColumnName("PeriodName");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.IsClosed).HasColumnName("IsClosed");
            this.Property(t => t.TargetAmount).HasColumnName("TargetAmount");
            this.Property(t => t.OutStandingAMT).HasColumnName("OutStandingAMT");
            this.Property(t => t.CollectedAmount).HasColumnName("CollectedAmount");
            this.Property(t => t.DebtorCount).HasColumnName("DebtorCount");
            this.Property(t => t.CaseCount).HasColumnName("CaseCount");

            // Relationships
            this.HasRequired(t => t.CM00005)
                .WithMany(t => t.CM00007)
                .HasForeignKey(d => d.CalendarID);

        }
    }
}
