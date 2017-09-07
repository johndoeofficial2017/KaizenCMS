using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00141Map : EntityTypeConfiguration<CM00141>
    {
        public CM00141Map()
        {
            // Primary Key
            this.HasKey(t => new { t.AddressCodeType, t.LegalID });

            // Properties
            this.Property(t => t.AddressCodeType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LegalID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.AddressName)
                .IsRequired()
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

            this.Property(t => t.Ext1)
                .HasMaxLength(5);

            this.Property(t => t.Ext2)
                .HasMaxLength(5);

            this.Property(t => t.Ext3)
                .HasMaxLength(5);

            this.Property(t => t.Ext4)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CM00141");
            this.Property(t => t.AddressCodeType).HasColumnName("AddressCodeType");
            this.Property(t => t.LegalID).HasColumnName("LegalID");
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
            this.Property(t => t.Ext1).HasColumnName("Ext1");
            this.Property(t => t.Ext2).HasColumnName("Ext2");
            this.Property(t => t.Ext3).HasColumnName("Ext3");
            this.Property(t => t.Ext4).HasColumnName("Ext4");

            // Relationships
            this.HasRequired(t => t.CM00034)
                .WithMany(t => t.CM00141)
                .HasForeignKey(d => d.AddressCodeType);
            this.HasRequired(t => t.CM00140)
                .WithMany(t => t.CM00141)
                .HasForeignKey(d => d.LegalID);

        }
    }
}
