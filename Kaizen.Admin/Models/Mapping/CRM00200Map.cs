using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class CRM00200Map : EntityTypeConfiguration<CRM00200>
    {
        public CRM00200Map()
        {
            // Primary Key
            this.HasKey(t => t.TransactionID);

            // Properties
            this.Property(t => t.TransactionIName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CRM00200");
            this.Property(t => t.TransactionID).HasColumnName("TransactionID");
            this.Property(t => t.TransactionIName).HasColumnName("TransactionIName");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.TransactionMessage).HasColumnName("TransactionMessage");
        }
    }
}
