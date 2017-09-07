using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00209Map : EntityTypeConfiguration<CM00209>
    {
        public CM00209Map()
        {
            // Primary Key
            this.HasKey(t => t.TransactionID);

            // Properties
            this.Property(t => t.TransactionID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ContractID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.EmployeeID)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00209");
            this.Property(t => t.TransactionID).HasColumnName("TransactionID");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.ActionPlanID).HasColumnName("ActionPlanID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.AssignDate).HasColumnName("AssignDate");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
        }
    }
}
