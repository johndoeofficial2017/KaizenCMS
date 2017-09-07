using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Prn00100Map : EntityTypeConfiguration<Prn00100>
    {
        public Prn00100Map()
        {
            // Primary Key
            this.HasKey(t => t.TrxID);

            // Properties
            this.Property(t => t.UserName)
                .HasMaxLength(50);

            this.Property(t => t.PCName)
                .HasMaxLength(50);

            this.Property(t => t.DisplayName)
                .HasMaxLength(50);

            this.Property(t => t.PCIP)
                .HasMaxLength(50);

            this.Property(t => t.PrinterName)
                .HasMaxLength(50);

            this.Property(t => t.PrintedFile)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Prn00100");
            this.Property(t => t.TrxID).HasColumnName("TrxID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.PCName).HasColumnName("PCName");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.PCIP).HasColumnName("PCIP");
            this.Property(t => t.PrinterName).HasColumnName("PrinterName");
            this.Property(t => t.PrintedFile).HasColumnName("PrintedFile");
        }
    }
}
