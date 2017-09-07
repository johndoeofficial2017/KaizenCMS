using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_CaseOldMap : EntityTypeConfiguration<CM_CaseOld>
    {
        public CM_CaseOldMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.ClientID });

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ClientName)
                .HasMaxLength(100);

            this.Property(t => t.CaseAddess)
                .HasMaxLength(500);

            this.Property(t => t.CaseAccountNo)
                .HasMaxLength(50);

            this.Property(t => t.InvoiceNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM_CaseOld");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.CaseAddess).HasColumnName("CaseAddess");
            this.Property(t => t.CaseAccountNo).HasColumnName("CaseAccountNo");
            this.Property(t => t.InvoiceNumber).HasColumnName("InvoiceNumber");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
        }
    }
}
