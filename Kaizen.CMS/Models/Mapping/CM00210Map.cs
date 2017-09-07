using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00210Map : EntityTypeConfiguration<CM00210>
    {
        public CM00210Map()
        {
            // Primary Key
            this.HasKey(t => t.CaseRef);

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TransactionID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.JecketsID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ProductID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseAddess)
                .HasMaxLength(500);

            this.Property(t => t.CaseContractDetail)
                .HasMaxLength(50);

            this.Property(t => t.CaseAccountNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00210");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.TransactionID).HasColumnName("TransactionID");
            this.Property(t => t.JecketsID).HasColumnName("JecketsID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.CaseTreeID).HasColumnName("CaseTreeID");
            this.Property(t => t.CaseAddess).HasColumnName("CaseAddess");
            this.Property(t => t.CaseContractDetail).HasColumnName("CaseContractDetail");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.CaseAccountNo).HasColumnName("CaseAccountNo");
            this.Property(t => t.ClosingDate).HasColumnName("ClosingDate");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.OriginalAmount).HasColumnName("OriginalAmount");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.PrincipleAmount).HasColumnName("PrincipleAmount");

            // Relationships
            this.HasRequired(t => t.CM00209)
                .WithMany(t => t.CM00210)
                .HasForeignKey(d => d.TransactionID);

        }
    }
}
