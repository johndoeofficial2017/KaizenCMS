using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00113Map : EntityTypeConfiguration<CM00113>
    {
        public CM00113Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ClientID, t.EFTTypeID, t.BankCode });

            // Properties
            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.EFTTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BankCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BankAccount)
                .HasMaxLength(50);

            this.Property(t => t.IBANNumber)
                .HasMaxLength(50);

            this.Property(t => t.OtherInfo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00113");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.EFTTypeID).HasColumnName("EFTTypeID");
            this.Property(t => t.BankCode).HasColumnName("BankCode");
            this.Property(t => t.BankAccount).HasColumnName("BankAccount");
            this.Property(t => t.IBANNumber).HasColumnName("IBANNumber");
            this.Property(t => t.OtherInfo).HasColumnName("OtherInfo");

            // Relationships
            this.HasRequired(t => t.CM00110)
                .WithMany(t => t.CM00113)
                .HasForeignKey(d => d.ClientID);

        }
    }
}
