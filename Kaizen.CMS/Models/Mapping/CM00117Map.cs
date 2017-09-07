using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00117Map : EntityTypeConfiguration<CM00117>
    {
        public CM00117Map()
        {
            // Primary Key
            this.HasKey(t => t.CommissionID);

            // Properties
            this.Property(t => t.YearCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TargetID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00117");
            this.Property(t => t.CommissionID).HasColumnName("CommissionID");
            this.Property(t => t.YearCode).HasColumnName("YearCode");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.PERIODID).HasColumnName("PERIODID");
            this.Property(t => t.TargetID).HasColumnName("TargetID");
            this.Property(t => t.TargetFrom).HasColumnName("TargetFrom");
            this.Property(t => t.TargetTo).HasColumnName("TargetTo");
            this.Property(t => t.CommissionAMT).HasColumnName("CommissionAMT");
            this.Property(t => t.CommissionPercent).HasColumnName("CommissionPercent");

            // Relationships
            this.HasRequired(t => t.CM00107)
                .WithMany(t => t.CM00117)
                .HasForeignKey(d => new { d.YearCode, d.AgentID, d.PERIODID, d.TargetID });

        }
    }
}
