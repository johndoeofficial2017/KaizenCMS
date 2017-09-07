using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Prn00101Map : EntityTypeConfiguration<Prn00101>
    {
        public Prn00101Map()
        {
            // Primary Key
            this.HasKey(t => t.PrnID);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PCName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DisplayName)
                .HasMaxLength(50);

            this.Property(t => t.PrnCatTypeName)
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
            this.ToTable("Prn00101");
            this.Property(t => t.PrnID).HasColumnName("PrnID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.PCName).HasColumnName("PCName");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.PrnCatTypeID).HasColumnName("PrnCatTypeID");
            this.Property(t => t.PrnCatTypeName).HasColumnName("PrnCatTypeName");
            this.Property(t => t.PCIP).HasColumnName("PCIP");
            this.Property(t => t.PrinterName).HasColumnName("PrinterName");
            this.Property(t => t.PrintedFile).HasColumnName("PrintedFile");
            this.Property(t => t.EmailToList).HasColumnName("EmailToList");
            this.Property(t => t.EmailCCList).HasColumnName("EmailCCList");
            this.Property(t => t.EmailBCCList).HasColumnName("EmailBCCList");

            // Relationships
            this.HasOptional(t => t.Prn00001)
                .WithMany(t => t.Prn00101)
                .HasForeignKey(d => d.PrnCatTypeID);

        }
    }
}
