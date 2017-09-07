using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_CaseDuplicateMap : EntityTypeConfiguration<CM_CaseDuplicate>
    {
        public CM_CaseDuplicateMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CaseRef, t.ClientID, t.DebtorID, t.UserName });

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ClientName)
                .HasMaxLength(100);

            this.Property(t => t.DebtorID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CaseAddess)
                .HasMaxLength(500);

            this.Property(t => t.CaseAccountNo)
                .HasMaxLength(50);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_CaseDuplicate");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.CaseAddess).HasColumnName("CaseAddess");
            this.Property(t => t.CaseAccountNo).HasColumnName("CaseAccountNo");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
