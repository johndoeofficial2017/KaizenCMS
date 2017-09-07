using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Prn00100Map : EntityTypeConfiguration<Prn00100>
    {
        public Prn00100Map()
        {
            // Primary Key
            this.HasKey(t => t.TrxID);

            // Properties
            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PCName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DisplayName)
                .HasMaxLength(50);

            this.Property(t => t.PCIP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PrinterName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PrintedFile)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmailToList)
                .HasMaxLength(50);

            this.Property(t => t.EmailCCList)
                .HasMaxLength(50);

            this.Property(t => t.EmailBCCList)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Prn00100");
            this.Property(t => t.TrxID).HasColumnName("TrxID");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.PCName).HasColumnName("PCName");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.PCIP).HasColumnName("PCIP");
            this.Property(t => t.PrinterName).HasColumnName("PrinterName");
            this.Property(t => t.PrintedFile).HasColumnName("PrintedFile");
            this.Property(t => t.EmailToList).HasColumnName("EmailToList");
            this.Property(t => t.EmailCCList).HasColumnName("EmailCCList");
            this.Property(t => t.EmailBCCList).HasColumnName("EmailBCCList");
        }
    }
}
