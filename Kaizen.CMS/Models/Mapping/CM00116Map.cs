using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00116Map : EntityTypeConfiguration<CM00116>
    {
        public CM00116Map()
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

            // Table & Column Mappings
            this.ToTable("CM00116");
            this.Property(t => t.CommissionID).HasColumnName("CommissionID");
            this.Property(t => t.YearCode).HasColumnName("YearCode");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.PERIODID).HasColumnName("PERIODID");
            this.Property(t => t.TargetFrom).HasColumnName("TargetFrom");
            this.Property(t => t.TargetTo).HasColumnName("TargetTo");
            this.Property(t => t.CommissionAMT).HasColumnName("CommissionAMT");
            this.Property(t => t.CommissionPercent).HasColumnName("CommissionPercent");

            // Relationships
            this.HasRequired(t => t.CM10110)
                .WithMany(t => t.CM00116)
                .HasForeignKey(d => new { d.YearCode, d.AgentID, d.PERIODID });

        }
    }
}
