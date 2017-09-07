using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00110Map : EntityTypeConfiguration<SOP00110>
    {
        public SOP00110Map()
        {
            // Primary Key
            this.HasKey(t => t.SalePersonID);

            // Properties
            this.Property(t => t.SalePersonID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserCode)
                .HasMaxLength(10);

            this.Property(t => t.EmployeeID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SupervisorID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.MidName)
                .HasMaxLength(50);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(100);

            this.Property(t => t.DirectPhon)
                .HasMaxLength(15);

            this.Property(t => t.PhonExtension)
                .HasMaxLength(5);

            this.Property(t => t.SiteID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SubBinID)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.SOPCUSTNMBR)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP00110");
            this.Property(t => t.SalePersonID).HasColumnName("SalePersonID");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.SupervisorID).HasColumnName("SupervisorID");
            this.Property(t => t.SalePersonTypeID).HasColumnName("SalePersonTypeID");
            this.Property(t => t.MidName).HasColumnName("MidName");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Inactive).HasColumnName("Inactive");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.DirectPhon).HasColumnName("DirectPhon");
            this.Property(t => t.PhonExtension).HasColumnName("PhonExtension");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.BinTrack).HasColumnName("BinTrack");
            this.Property(t => t.SubBinID).HasColumnName("SubBinID");
            this.Property(t => t.BinID).HasColumnName("BinID");
            this.Property(t => t.SOPCUSTNMBR).HasColumnName("SOPCUSTNMBR");

            // Relationships
            this.HasRequired(t => t.SOP00008)
                .WithMany(t => t.SOP00110)
                .HasForeignKey(d => d.SalePersonTypeID);

        }
    }
}
