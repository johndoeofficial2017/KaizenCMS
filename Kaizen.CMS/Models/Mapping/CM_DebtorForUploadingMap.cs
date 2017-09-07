using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_DebtorForUploadingMap : EntityTypeConfiguration<CM_DebtorForUploading>
    {
        public CM_DebtorForUploadingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DebtorID, t.DebtorName, t.GenderID, t.AddressCode, t.TempDebtorID, t.TempDebtorName });

            // Properties
            this.Property(t => t.AddressName)
                .HasMaxLength(300);

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
                .HasMaxLength(300);

            this.Property(t => t.Pnone02)
                .HasMaxLength(300);

            this.Property(t => t.Pnone03)
                .HasMaxLength(300);

            this.Property(t => t.Pnone04)
                .HasMaxLength(300);

            this.Property(t => t.MobileNo1)
                .HasMaxLength(100);

            this.Property(t => t.MobileNo2)
                .HasMaxLength(100);

            this.Property(t => t.MobileNo3)
                .HasMaxLength(100);

            this.Property(t => t.MobileNo4)
                .HasMaxLength(100);

            this.Property(t => t.POBox)
                .HasMaxLength(100);

            this.Property(t => t.Other01)
                .HasMaxLength(100);

            this.Property(t => t.Other02)
                .HasMaxLength(100);

            this.Property(t => t.Other03)
                .HasMaxLength(100);

            this.Property(t => t.Other04)
                .HasMaxLength(100);

            this.Property(t => t.Address1)
                .HasMaxLength(300);

            this.Property(t => t.Address2)
                .HasMaxLength(300);

            this.Property(t => t.Address3)
                .HasMaxLength(300);

            this.Property(t => t.Address4)
                .HasMaxLength(300);

            this.Property(t => t.Email02)
                .HasMaxLength(100);

            this.Property(t => t.Email01)
                .HasMaxLength(100);

            this.Property(t => t.Email03)
                .HasMaxLength(100);

            this.Property(t => t.Email04)
                .HasMaxLength(100);

            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.DebtorName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Occupation)
                .HasMaxLength(50);

            this.Property(t => t.GeneralName)
                .HasMaxLength(50);

            this.Property(t => t.ShortName)
                .HasMaxLength(50);

            this.Property(t => t.CPRCRNo)
                .HasMaxLength(50);

            this.Property(t => t.PassportNumber)
                .HasMaxLength(25);

            this.Property(t => t.ResidenceNo)
                .HasMaxLength(25);

            this.Property(t => t.EmployerName)
                .HasMaxLength(1000);

            this.Property(t => t.NationalityID)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.AddressCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TempDebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.TempDebtorName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TempShortName)
                .HasMaxLength(50);

            this.Property(t => t.TempGeneralName)
                .HasMaxLength(50);

            this.Property(t => t.TempOccupation)
                .HasMaxLength(50);

            this.Property(t => t.TempCPRCRNo)
                .HasMaxLength(50);

            this.Property(t => t.TempPassportNumber)
                .HasMaxLength(25);

            this.Property(t => t.TempResidenceNo)
                .HasMaxLength(25);

            this.Property(t => t.TempEmployerName)
                .HasMaxLength(1000);

            this.Property(t => t.TempNationalityID)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.TempAddressName)
                .HasMaxLength(300);

            this.Property(t => t.TempWebSite)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TempCountryID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TempCityID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TempPnone01)
                .HasMaxLength(300);

            this.Property(t => t.TemppPnone02)
                .HasMaxLength(300);

            this.Property(t => t.TempPnone03)
                .HasMaxLength(300);

            this.Property(t => t.TempPnone04)
                .HasMaxLength(300);

            this.Property(t => t.TempMobileNo1)
                .HasMaxLength(100);

            this.Property(t => t.TempMobileNo2)
                .HasMaxLength(100);

            this.Property(t => t.TempMobileNo3)
                .HasMaxLength(100);

            this.Property(t => t.TempMobileNo4)
                .HasMaxLength(100);

            this.Property(t => t.TempPOBox)
                .HasMaxLength(100);

            this.Property(t => t.TempOther01)
                .HasMaxLength(100);

            this.Property(t => t.TempOther02)
                .HasMaxLength(100);

            this.Property(t => t.TempOther03)
                .HasMaxLength(100);

            this.Property(t => t.TempOther04)
                .HasMaxLength(100);

            this.Property(t => t.TempAddress1)
                .HasMaxLength(300);

            this.Property(t => t.TempAddress2)
                .HasMaxLength(300);

            this.Property(t => t.TempAddress3)
                .HasMaxLength(300);

            this.Property(t => t.TempAddress4)
                .HasMaxLength(300);

            this.Property(t => t.TempEmail01)
                .HasMaxLength(100);

            this.Property(t => t.TempEmail02)
                .HasMaxLength(100);

            this.Property(t => t.TempEmail03)
                .HasMaxLength(100);

            this.Property(t => t.TempEmail04)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("CM_DebtorForUploading");
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
            this.Property(t => t.Email02).HasColumnName("Email02");
            this.Property(t => t.Email01).HasColumnName("Email01");
            this.Property(t => t.Email03).HasColumnName("Email03");
            this.Property(t => t.Email04).HasColumnName("Email04");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.DebtorName).HasColumnName("DebtorName");
            this.Property(t => t.GenderID).HasColumnName("GenderID");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.Occupation).HasColumnName("Occupation");
            this.Property(t => t.GeneralName).HasColumnName("GeneralName");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.DebtorDescription).HasColumnName("DebtorDescription");
            this.Property(t => t.CPRCRNo).HasColumnName("CPRCRNo");
            this.Property(t => t.PassportNumber).HasColumnName("PassportNumber");
            this.Property(t => t.ResidenceNo).HasColumnName("ResidenceNo");
            this.Property(t => t.EmployerName).HasColumnName("EmployerName");
            this.Property(t => t.NationalityID).HasColumnName("NationalityID");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.TempDebtorID).HasColumnName("TempDebtorID");
            this.Property(t => t.TempDebtorName).HasColumnName("TempDebtorName");
            this.Property(t => t.TempShortName).HasColumnName("TempShortName");
            this.Property(t => t.TempGeneralName).HasColumnName("TempGeneralName");
            this.Property(t => t.TempDebtorDescription).HasColumnName("TempDebtorDescription");
            this.Property(t => t.TempGenderID).HasColumnName("TempGenderID");
            this.Property(t => t.TempBirthDate).HasColumnName("TempBirthDate");
            this.Property(t => t.TempOccupation).HasColumnName("TempOccupation");
            this.Property(t => t.TempCPRCRNo).HasColumnName("TempCPRCRNo");
            this.Property(t => t.TempPassportNumber).HasColumnName("TempPassportNumber");
            this.Property(t => t.TempResidenceNo).HasColumnName("TempResidenceNo");
            this.Property(t => t.TempEmployerName).HasColumnName("TempEmployerName");
            this.Property(t => t.TempNationalityID).HasColumnName("TempNationalityID");
            this.Property(t => t.TempAddressName).HasColumnName("TempAddressName");
            this.Property(t => t.TempWebSite).HasColumnName("TempWebSite");
            this.Property(t => t.TempCountryID).HasColumnName("TempCountryID");
            this.Property(t => t.TempCityID).HasColumnName("TempCityID");
            this.Property(t => t.TempPnone01).HasColumnName("TempPnone01");
            this.Property(t => t.TemppPnone02).HasColumnName("TemppPnone02");
            this.Property(t => t.TempPnone03).HasColumnName("TempPnone03");
            this.Property(t => t.TempPnone04).HasColumnName("TempPnone04");
            this.Property(t => t.TempMobileNo1).HasColumnName("TempMobileNo1");
            this.Property(t => t.TempMobileNo2).HasColumnName("TempMobileNo2");
            this.Property(t => t.TempMobileNo3).HasColumnName("TempMobileNo3");
            this.Property(t => t.TempMobileNo4).HasColumnName("TempMobileNo4");
            this.Property(t => t.TempPOBox).HasColumnName("TempPOBox");
            this.Property(t => t.TempOther01).HasColumnName("TempOther01");
            this.Property(t => t.TempOther02).HasColumnName("TempOther02");
            this.Property(t => t.TempOther03).HasColumnName("TempOther03");
            this.Property(t => t.TempOther04).HasColumnName("TempOther04");
            this.Property(t => t.TempAddress1).HasColumnName("TempAddress1");
            this.Property(t => t.TempAddress2).HasColumnName("TempAddress2");
            this.Property(t => t.TempAddress3).HasColumnName("TempAddress3");
            this.Property(t => t.TempAddress4).HasColumnName("TempAddress4");
            this.Property(t => t.TempEmail01).HasColumnName("TempEmail01");
            this.Property(t => t.TempEmail02).HasColumnName("TempEmail02");
            this.Property(t => t.TempEmail03).HasColumnName("TempEmail03");
            this.Property(t => t.TempEmail04).HasColumnName("TempEmail04");
        }
    }
}
