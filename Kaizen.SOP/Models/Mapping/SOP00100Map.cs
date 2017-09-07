using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00100Map : EntityTypeConfiguration<SOP00100>
    {
        public SOP00100Map()
        {
            // Primary Key
            this.HasKey(t => t.CUSTNMBR);

            // Properties
            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.GroupID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CurrencyCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PriorityID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTNAME)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ShortName)
                .HasMaxLength(50);

            this.Property(t => t.CUSTCLAS)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(5);

            this.Property(t => t.ParentCustomer)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.CPRCRNo)
                .HasMaxLength(50);

            this.Property(t => t.EmployerName)
                .HasMaxLength(20);

            this.Property(t => t.NationalityID)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.ShipTo)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.BillTo)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.StatementTo)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.AddressTypeCode)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.ContactTypeID)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.SalePersonID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.KaizenID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("SOP00100");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.StatementCycleID).HasColumnName("StatementCycleID");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.CUSTNAME).HasColumnName("CUSTNAME");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.CUSTCLAS).HasColumnName("CUSTCLAS");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.IsHold).HasColumnName("IsHold");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CustomerDescription).HasColumnName("CustomerDescription");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.ParentCustomer).HasColumnName("ParentCustomer");
            this.Property(t => t.CPRCRNo).HasColumnName("CPRCRNo");
            this.Property(t => t.EmployerName).HasColumnName("EmployerName");
            this.Property(t => t.NationalityID).HasColumnName("NationalityID");
            this.Property(t => t.ShipTo).HasColumnName("ShipTo");
            this.Property(t => t.BillTo).HasColumnName("BillTo");
            this.Property(t => t.StatementTo).HasColumnName("StatementTo");
            this.Property(t => t.AddressTypeCode).HasColumnName("AddressTypeCode");
            this.Property(t => t.ContactTypeID).HasColumnName("ContactTypeID");
            this.Property(t => t.PriceLevelCode).HasColumnName("PriceLevelCode");
            this.Property(t => t.SalePersonID).HasColumnName("SalePersonID");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.EntryDate).HasColumnName("EntryDate");
            this.Property(t => t.KaizenID).HasColumnName("KaizenID");

            // Relationships
            this.HasOptional(t => t.SOP00010)
                .WithMany(t => t.SOP00100)
                .HasForeignKey(d => d.CUSTCLAS);
            this.HasOptional(t => t.SOP00011)
                .WithMany(t => t.SOP00100)
                .HasForeignKey(d => d.GroupID);
            this.HasOptional(t => t.SOP00012)
                .WithMany(t => t.SOP00100)
                .HasForeignKey(d => d.PriorityID);
            this.HasOptional(t => t.SOP00013)
                .WithMany(t => t.SOP00100)
                .HasForeignKey(d => d.StatementCycleID);
            this.HasOptional(t => t.SOP00014)
                .WithMany(t => t.SOP00100)
                .HasForeignKey(d => d.StatusID);
            this.HasOptional(t => t.Sys00030)
                .WithMany(t => t.SOP00100)
                .HasForeignKey(d => d.NationalityID);

        }
    }
}
