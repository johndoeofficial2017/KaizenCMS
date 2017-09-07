using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_ClientMap : EntityTypeConfiguration<CM_Client>
    {
        public CM_ClientMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ClientID, t.StatusID, t.CUSTCLAS });

            // Properties
            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ClientName)
                .HasMaxLength(100);

            this.Property(t => t.ShortName)
                .HasMaxLength(50);

            this.Property(t => t.ContactPerson)
                .HasMaxLength(50);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(50);

            this.Property(t => t.StatusName)
                .HasMaxLength(15);

            this.Property(t => t.CUSTCLASNAME)
                .HasMaxLength(50);

            this.Property(t => t.PrefixValue)
                .HasMaxLength(5);

            this.Property(t => t.StatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CUSTCLAS)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AddressName)
                .HasMaxLength(100);

            this.Property(t => t.CountryID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CityID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.WebSite)
                .HasMaxLength(50);

            this.Property(t => t.Pnone01)
                .HasMaxLength(1000);

            this.Property(t => t.Pnone02)
                .HasMaxLength(1000);

            this.Property(t => t.Pnone03)
                .HasMaxLength(1000);

            this.Property(t => t.Pnone04)
                .HasMaxLength(1000);

            this.Property(t => t.MobileNo1)
                .HasMaxLength(1000);

            this.Property(t => t.MobileNo2)
                .HasMaxLength(1000);

            this.Property(t => t.MobileNo3)
                .HasMaxLength(1000);

            this.Property(t => t.MobileNo4)
                .HasMaxLength(1000);

            this.Property(t => t.POBox)
                .HasMaxLength(1000);

            this.Property(t => t.Other01)
                .HasMaxLength(1000);

            this.Property(t => t.Other02)
                .HasMaxLength(1000);

            this.Property(t => t.Other03)
                .HasMaxLength(1000);

            this.Property(t => t.Other04)
                .HasMaxLength(1000);

            this.Property(t => t.Address1)
                .HasMaxLength(1000);

            this.Property(t => t.Address2)
                .HasMaxLength(1000);

            this.Property(t => t.Address3)
                .HasMaxLength(1000);

            this.Property(t => t.Address4)
                .HasMaxLength(1000);

            this.Property(t => t.Email01)
                .HasMaxLength(1000);

            this.Property(t => t.Email02)
                .HasMaxLength(1000);

            this.Property(t => t.Email03)
                .HasMaxLength(1000);

            this.Property(t => t.Email04)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("CM_Client");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.ClientDescription).HasColumnName("ClientDescription");
            this.Property(t => t.ContactPerson).HasColumnName("ContactPerson");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.BillAddressCode).HasColumnName("BillAddressCode");
            this.Property(t => t.RemitToAddressCode).HasColumnName("RemitToAddressCode");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
            this.Property(t => t.CUSTCLASNAME).HasColumnName("CUSTCLASNAME");
            this.Property(t => t.PrefixValue).HasColumnName("PrefixValue");
            this.Property(t => t.Prefixlengh).HasColumnName("Prefixlengh");
            this.Property(t => t.LastClientID).HasColumnName("LastClientID");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.CUSTCLAS).HasColumnName("CUSTCLAS");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CityID).HasColumnName("CityID");
            this.Property(t => t.WebSite).HasColumnName("WebSite");
            this.Property(t => t.Pnone01).HasColumnName("Pnone01");
            this.Property(t => t.Pnone02).HasColumnName("Pnone02");
            this.Property(t => t.Pnone03).HasColumnName("Pnone03");
            this.Property(t => t.Pnone04).HasColumnName("Pnone04");
            this.Property(t => t.MobileNo1).HasColumnName("MobileNo1");
            this.Property(t => t.MobileNo2).HasColumnName("MobileNo2");
            this.Property(t => t.MobileNo3).HasColumnName("MobileNo3");
            this.Property(t => t.MobileNo4).HasColumnName("MobileNo4");
            this.Property(t => t.POBox).HasColumnName("POBox");
            this.Property(t => t.Other01).HasColumnName("Other01");
            this.Property(t => t.Other02).HasColumnName("Other02");
            this.Property(t => t.Other03).HasColumnName("Other03");
            this.Property(t => t.Other04).HasColumnName("Other04");
            this.Property(t => t.Address1).HasColumnName("Address1");
            this.Property(t => t.Address2).HasColumnName("Address2");
            this.Property(t => t.Address3).HasColumnName("Address3");
            this.Property(t => t.Address4).HasColumnName("Address4");
            this.Property(t => t.Email01).HasColumnName("Email01");
            this.Property(t => t.Email02).HasColumnName("Email02");
            this.Property(t => t.Email03).HasColumnName("Email03");
            this.Property(t => t.Email04).HasColumnName("Email04");
        }
    }
}
