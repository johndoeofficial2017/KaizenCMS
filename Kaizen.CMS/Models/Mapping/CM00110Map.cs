using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00110Map : EntityTypeConfiguration<CM00110>
    {
        public CM00110Map()
        {
            // Primary Key
            this.HasKey(t => t.ClientID);

            // Properties
            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ClientName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ShortName)
                .HasMaxLength(50);

            this.Property(t => t.NationaltyID)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.NationalityName)
                .HasMaxLength(50);

            this.Property(t => t.ParentClientID)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ExtraField)
                .HasMaxLength(50);

            this.Property(t => t.CUSTCLAS)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.StatusID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00110");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.ClientDescription).HasColumnName("ClientDescription");
            this.Property(t => t.NationaltyID).HasColumnName("NationaltyID");
            this.Property(t => t.NationalityName).HasColumnName("NationalityName");
            this.Property(t => t.IsJoint).HasColumnName("IsJoint");
            this.Property(t => t.ParentClientID).HasColumnName("ParentClientID");
            this.Property(t => t.ExtraField).HasColumnName("ExtraField");
            this.Property(t => t.CUSTCLAS).HasColumnName("CUSTCLAS");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.ContactTypeID).HasColumnName("ContactTypeID");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.BillAddressCode).HasColumnName("BillAddressCode");
            this.Property(t => t.RemitToAddressCode).HasColumnName("RemitToAddressCode");

            // Relationships
            this.HasRequired(t => t.CM00021)
                .WithMany(t => t.CM00110)
                .HasForeignKey(d => d.CUSTCLAS);
            this.HasRequired(t => t.CM00022)
                .WithMany(t => t.CM00110)
                .HasForeignKey(d => d.StatusID);

        }
    }
}
