using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00120Map : EntityTypeConfiguration<CM00120>
    {
        public CM00120Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ClientID, t.PERIODID });

            // Properties
            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.PERIODID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM00120");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.PERIODID).HasColumnName("PERIODID");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.TotalWriteOff).HasColumnName("TotalWriteOff");
            this.Property(t => t.FinanceCharge).HasColumnName("FinanceCharge");
            this.Property(t => t.TotalCallactedAMT).HasColumnName("TotalCallactedAMT");
            this.Property(t => t.TotalCaseNumber).HasColumnName("TotalCaseNumber");

            // Relationships
            this.HasRequired(t => t.CM00007)
                .WithMany(t => t.CM00120)
                .HasForeignKey(d => d.PERIODID);
            this.HasRequired(t => t.CM00110)
                .WithMany(t => t.CM00120)
                .HasForeignKey(d => d.ClientID);

        }
    }
}
