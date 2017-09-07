using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00104Map : EntityTypeConfiguration<CM00104>
    {
        public CM00104Map()
        {
            // Primary Key
            this.HasKey(t => new { t.AddressCode, t.DebtorID });

            // Properties
            this.Property(t => t.AddressCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.AddressName)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.WebSite)
                .HasMaxLength(1000);

            this.Property(t => t.CountryID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CityID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Phone01)
                .HasMaxLength(1000);

            this.Property(t => t.Phone02)
                .HasMaxLength(1000);

            this.Property(t => t.Phone03)
                .HasMaxLength(1000);

            this.Property(t => t.Phone04)
                .HasMaxLength(1000);

            this.Property(t => t.Ext1)
                .HasMaxLength(1000);

            this.Property(t => t.Ext2)
                .HasMaxLength(1000);

            this.Property(t => t.Ext3)
                .HasMaxLength(1000);

            this.Property(t => t.Ext4)
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
                .HasMaxLength(50);

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

            this.Property(t => t.FlatNo)
                .HasMaxLength(100);

            this.Property(t => t.BuildingNo)
                .HasMaxLength(100);

            this.Property(t => t.RoadName)
                .HasMaxLength(100);

            this.Property(t => t.RoadNo)
                .HasMaxLength(100);

            this.Property(t => t.BlockNo)
                .HasMaxLength(100);

            this.Property(t => t.BlockName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("CM00104");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
            this.Property(t => t.IsReadOnlyData).HasColumnName("IsReadOnlyData");
            this.Property(t => t.WebSite).HasColumnName("WebSite");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CityID).HasColumnName("CityID");
            this.Property(t => t.Phone01).HasColumnName("Phone01");
            this.Property(t => t.Phone02).HasColumnName("Phone02");
            this.Property(t => t.Phone03).HasColumnName("Phone03");
            this.Property(t => t.Phone04).HasColumnName("Phone04");
            this.Property(t => t.Ext1).HasColumnName("Ext1");
            this.Property(t => t.Ext2).HasColumnName("Ext2");
            this.Property(t => t.Ext3).HasColumnName("Ext3");
            this.Property(t => t.Ext4).HasColumnName("Ext4");
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
            this.Property(t => t.FlatNo).HasColumnName("FlatNo");
            this.Property(t => t.BuildingNo).HasColumnName("BuildingNo");
            this.Property(t => t.RoadName).HasColumnName("RoadName");
            this.Property(t => t.RoadNo).HasColumnName("RoadNo");
            this.Property(t => t.BlockNo).HasColumnName("BlockNo");
            this.Property(t => t.BlockName).HasColumnName("BlockName");

            // Relationships
            this.HasRequired(t => t.CM00009)
                .WithMany(t => t.CM00104)
                .HasForeignKey(d => d.AddressCode);
            this.HasRequired(t => t.CM00100)
                .WithMany(t => t.CM00104)
                .HasForeignKey(d => d.DebtorID);
        }
    }
}
