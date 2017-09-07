using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00101Map : EntityTypeConfiguration<SOP00101>
    {
        public SOP00101Map()
        {
            // Primary Key
            this.HasKey(t => new { t.AddressTypeCode, t.CUSTNMBR });

            // Properties
            this.Property(t => t.AddressTypeCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AddressName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.WebSite)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CountryID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CityID)
                .IsFixedLength()
                .HasMaxLength(10);

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
            this.ToTable("SOP00101");
            this.Property(t => t.AddressTypeCode).HasColumnName("AddressTypeCode");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
            this.Property(t => t.WebSite).HasColumnName("WebSite");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CityID).HasColumnName("CityID");
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

            // Relationships
            this.HasRequired(t => t.SOP00009)
                .WithMany(t => t.SOP00101)
                .HasForeignKey(d => d.AddressTypeCode);
            this.HasRequired(t => t.SOP00100)
                .WithMany(t => t.SOP00101)
                .HasForeignKey(d => d.CUSTNMBR);
            this.HasOptional(t => t.Sys00013)
                .WithMany(t => t.SOP00101)
                .HasForeignKey(d => d.CountryID);
            this.HasOptional(t => t.Sys00014)
                .WithMany(t => t.SOP00101)
                .HasForeignKey(d => d.CityID);

        }
    }
}
