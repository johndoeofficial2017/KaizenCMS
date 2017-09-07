using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00040Map : EntityTypeConfiguration<Kaizen00040>
    {
        public Kaizen00040Map()
        {
            // Primary Key
            this.HasKey(t => new { t.SMSAccountCode, t.CompanyID });

            // Properties
            this.Property(t => t.SMSAccountCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SMSAccountName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen00040");
            this.Property(t => t.SMSAccountCode).HasColumnName("SMSAccountCode");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.SMSAccountName).HasColumnName("SMSAccountName");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Kaizen00040)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
