using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00100Map : EntityTypeConfiguration<CM00100>
    {
        public CM00100Map()
        {
            // Primary Key
            this.HasKey(t => t.DebtorID);

            // Properties
            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.PLACE_OF_BIRTH)
                .HasMaxLength(200);

            this.Property(t => t.COUNTRY_OF_BIRTH)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.MaritalID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.FirstNameEnglish)
                .HasMaxLength(200);

            this.Property(t => t.FirstNameArabic)
                .HasMaxLength(1000);

            this.Property(t => t.EnglishFullName)
                .HasMaxLength(200);

            this.Property(t => t.ArabicFullName)
                .HasMaxLength(200);

            this.Property(t => t.SHORT_NAME_ARAB)
                .HasMaxLength(1000);

            this.Property(t => t.SHORT_NAME_ENG)
                .HasMaxLength(1000);

            this.Property(t => t.MiddleName1English)
                .HasMaxLength(200);

            this.Property(t => t.MiddleName2English)
                .HasMaxLength(200);

            this.Property(t => t.MiddleName3English)
                .HasMaxLength(200);

            this.Property(t => t.MiddleName4English)
                .HasMaxLength(200);

            this.Property(t => t.MiddleName1Arabic)
                .HasMaxLength(200);

            this.Property(t => t.MiddleName2Arabic)
                .HasMaxLength(200);

            this.Property(t => t.MiddleName3Arabic)
                .HasMaxLength(200);

            this.Property(t => t.MOTHER_FIRST_NAME)
                .HasMaxLength(200);

            this.Property(t => t.MOTHER_LAST_NAME)
                .HasMaxLength(200);

            this.Property(t => t.ResidenceNo)
                .HasMaxLength(200);

            this.Property(t => t.GovernorateNo)
                .HasMaxLength(200);

            this.Property(t => t.GovernorateNameEnglish)
                .HasMaxLength(200);

            this.Property(t => t.EmployerFlag1)
                .HasMaxLength(200);

            this.Property(t => t.EmployerName1)
                .HasMaxLength(200);

            this.Property(t => t.EmployerNo1)
                .HasMaxLength(200);

            this.Property(t => t.EmployerNameArabic)
                .HasMaxLength(200);

            this.Property(t => t.EmploymentFlag)
                .HasMaxLength(200);

            this.Property(t => t.LaborForceParticipation)
                .HasMaxLength(200);

            this.Property(t => t.LatestEducationDegree)
                .HasMaxLength(200);

            this.Property(t => t.EmploymentNameEnglish)
                .HasMaxLength(200);

            this.Property(t => t.OccupationDescription1)
                .HasMaxLength(200);

            this.Property(t => t.OccupationEnglish)
                .HasMaxLength(200);

            this.Property(t => t.SponsorCPRNoorUnitNo)
                .HasMaxLength(200);

            this.Property(t => t.SponsorFlag)
                .HasMaxLength(200);

            this.Property(t => t.SponsorName)
                .HasMaxLength(200);

            this.Property(t => t.SponserId)
                .HasMaxLength(200);

            this.Property(t => t.SponserNameEnglish)
                .HasMaxLength(200);

            this.Property(t => t.FingerprintCode)
                .HasMaxLength(200);

            this.Property(t => t.IdNumber)
                .HasMaxLength(200);

            this.Property(t => t.ParentDebtor)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ReligionID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CUSTCLAS)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(4);

            this.Property(t => t.SignatureExtension)
                .HasMaxLength(4);

            this.Property(t => t.CPRExpiryDate)
                .HasMaxLength(50);

            this.Property(t => t.CPRIssueDate)
                .HasMaxLength(50);

            this.Property(t => t.CPRSerialNumber)
                .HasMaxLength(50);

            this.Property(t => t.CPRCRNo)
                .HasMaxLength(50);

            this.Property(t => t.PassportNumber)
                .HasMaxLength(50);

            this.Property(t => t.PassportSequnceNo)
                .HasMaxLength(50);

            this.Property(t => t.PassportType)
                .HasMaxLength(50);

            this.Property(t => t.VisaNo)
                .HasMaxLength(50);

            this.Property(t => t.VisaType)
                .HasMaxLength(50);

            this.Property(t => t.ResidentPermitNo)
                .HasMaxLength(50);

            this.Property(t => t.TypeOfResident)
                .HasMaxLength(50);

            this.Property(t => t.NationalityID)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.TxtField01)
                .HasMaxLength(50);

            this.Property(t => t.TxtField02)
                .HasMaxLength(50);

            this.Property(t => t.TxtField03)
                .HasMaxLength(50);

            this.Property(t => t.TxtField04)
                .HasMaxLength(50);

            this.Property(t => t.AddressCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AddressEnglish)
                .HasMaxLength(1000);

            this.Property(t => t.AddressArabic)
                .HasMaxLength(1000);

            this.Property(t => t.AddressName)
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
            this.ToTable("CM00100");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.GenderID).HasColumnName("GenderID");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.PLACE_OF_BIRTH).HasColumnName("PLACE_OF_BIRTH");
            this.Property(t => t.COUNTRY_OF_BIRTH).HasColumnName("COUNTRY_OF_BIRTH");
            this.Property(t => t.IsJoint).HasColumnName("IsJoint");
            this.Property(t => t.MaritalID).HasColumnName("MaritalID");
            this.Property(t => t.DebtorDescription).HasColumnName("DebtorDescription");
            this.Property(t => t.FirstNameEnglish).HasColumnName("FirstNameEnglish");
            this.Property(t => t.FirstNameArabic).HasColumnName("FirstNameArabic");
            this.Property(t => t.EnglishFullName).HasColumnName("EnglishFullName");
            this.Property(t => t.ArabicFullName).HasColumnName("ArabicFullName");
            this.Property(t => t.SHORT_NAME_ARAB).HasColumnName("SHORT_NAME_ARAB");
            this.Property(t => t.SHORT_NAME_ENG).HasColumnName("SHORT_NAME_ENG");
            this.Property(t => t.MiddleName1English).HasColumnName("MiddleName1English");
            this.Property(t => t.MiddleName2English).HasColumnName("MiddleName2English");
            this.Property(t => t.MiddleName3English).HasColumnName("MiddleName3English");
            this.Property(t => t.MiddleName4English).HasColumnName("MiddleName4English");
            this.Property(t => t.MiddleName1Arabic).HasColumnName("MiddleName1Arabic");
            this.Property(t => t.MiddleName2Arabic).HasColumnName("MiddleName2Arabic");
            this.Property(t => t.MiddleName3Arabic).HasColumnName("MiddleName3Arabic");
            this.Property(t => t.MOTHER_FIRST_NAME).HasColumnName("MOTHER_FIRST_NAME");
            this.Property(t => t.MOTHER_LAST_NAME).HasColumnName("MOTHER_LAST_NAME");
            this.Property(t => t.ContactTypeID).HasColumnName("ContactTypeID");
            this.Property(t => t.ResidenceNo).HasColumnName("ResidenceNo");
            this.Property(t => t.GovernorateNo).HasColumnName("GovernorateNo");
            this.Property(t => t.GovernorateNameEnglish).HasColumnName("GovernorateNameEnglish");
            this.Property(t => t.EmployerFlag1).HasColumnName("EmployerFlag1");
            this.Property(t => t.EmployerName1).HasColumnName("EmployerName1");
            this.Property(t => t.EmployerNo1).HasColumnName("EmployerNo1");
            this.Property(t => t.EmployerNameArabic).HasColumnName("EmployerNameArabic");
            this.Property(t => t.EmploymentFlag).HasColumnName("EmploymentFlag");
            this.Property(t => t.LaborForceParticipation).HasColumnName("LaborForceParticipation");
            this.Property(t => t.LatestEducationDegree).HasColumnName("LatestEducationDegree");
            this.Property(t => t.EmploymentNameEnglish).HasColumnName("EmploymentNameEnglish");
            this.Property(t => t.OccupationDescription1).HasColumnName("OccupationDescription1");
            this.Property(t => t.OccupationEnglish).HasColumnName("OccupationEnglish");
            this.Property(t => t.SponsorCPRNoorUnitNo).HasColumnName("SponsorCPRNoorUnitNo");
            this.Property(t => t.SponsorFlag).HasColumnName("SponsorFlag");
            this.Property(t => t.SponsorName).HasColumnName("SponsorName");
            this.Property(t => t.SponserId).HasColumnName("SponserId");
            this.Property(t => t.SponserNameEnglish).HasColumnName("SponserNameEnglish");
            this.Property(t => t.FingerprintCode).HasColumnName("FingerprintCode");
            this.Property(t => t.IdNumber).HasColumnName("IdNumber");
            this.Property(t => t.ParentDebtor).HasColumnName("ParentDebtor");
            this.Property(t => t.ReligionID).HasColumnName("ReligionID");
            this.Property(t => t.CUSTCLAS).HasColumnName("CUSTCLAS");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.DebtorStatusID).HasColumnName("DebtorStatusID");
            this.Property(t => t.IsHold).HasColumnName("IsHold");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.SignatureExtension).HasColumnName("SignatureExtension");
            this.Property(t => t.CPRExpiryDate).HasColumnName("CPRExpiryDate");
            this.Property(t => t.CPRIssueDate).HasColumnName("CPRIssueDate");
            this.Property(t => t.CPRSerialNumber).HasColumnName("CPRSerialNumber");
            this.Property(t => t.CPRCRNo).HasColumnName("CPRCRNo");
            this.Property(t => t.PassportExpiryDate).HasColumnName("PassportExpiryDate");
            this.Property(t => t.PassportIssueDate).HasColumnName("PassportIssueDate");
            this.Property(t => t.PassportNumber).HasColumnName("PassportNumber");
            this.Property(t => t.PassportSequnceNo).HasColumnName("PassportSequnceNo");
            this.Property(t => t.PassportType).HasColumnName("PassportType");
            this.Property(t => t.VisaNo).HasColumnName("VisaNo");
            this.Property(t => t.VisaExpiryDate).HasColumnName("VisaExpiryDate");
            this.Property(t => t.VisaType).HasColumnName("VisaType");
            this.Property(t => t.ResidentPermitNo).HasColumnName("ResidentPermitNo");
            this.Property(t => t.ResidentPermitExpiryDate).HasColumnName("ResidentPermitExpiryDate");
            this.Property(t => t.TypeOfResident).HasColumnName("TypeOfResident");
            this.Property(t => t.NationalityID).HasColumnName("NationalityID");
            this.Property(t => t.NO_OF_DEPENDENTS).HasColumnName("NO_OF_DEPENDENTS");
            this.Property(t => t.MonthlySalary).HasColumnName("MonthlySalary");
            this.Property(t => t.AMT01).HasColumnName("AMT01");
            this.Property(t => t.AMT02).HasColumnName("AMT02");
            this.Property(t => t.AMT03).HasColumnName("AMT03");
            this.Property(t => t.TxtField01).HasColumnName("TxtField01");
            this.Property(t => t.TxtField02).HasColumnName("TxtField02");
            this.Property(t => t.TxtField03).HasColumnName("TxtField03");
            this.Property(t => t.TxtField04).HasColumnName("TxtField04");
            this.Property(t => t.CheckBoxField01).HasColumnName("CheckBoxField01");
            this.Property(t => t.CheckBoxField02).HasColumnName("CheckBoxField02");
            this.Property(t => t.CheckBoxField03).HasColumnName("CheckBoxField03");
            this.Property(t => t.CheckBoxField04).HasColumnName("CheckBoxField04");
            this.Property(t => t.Date01).HasColumnName("Date01");
            this.Property(t => t.Date02).HasColumnName("Date02");
            this.Property(t => t.Date03).HasColumnName("Date03");
            this.Property(t => t.Date04).HasColumnName("Date04");
            this.Property(t => t.AddressCode).HasColumnName("AddressCode");
            this.Property(t => t.AddressEnglish).HasColumnName("AddressEnglish");
            this.Property(t => t.AddressArabic).HasColumnName("AddressArabic");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
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
            this.HasOptional(t => t.CM00010)
                .WithMany(t => t.CM00100)
                .HasForeignKey(d => d.CUSTCLAS);
            this.HasOptional(t => t.CM00011)
                .WithMany(t => t.CM00100)
                .HasForeignKey(d => d.GroupID);
            this.HasOptional(t => t.CM00012)
                .WithMany(t => t.CM00100)
                .HasForeignKey(d => d.PriorityID);
            this.HasOptional(t => t.CM00014)
                .WithMany(t => t.CM00100)
                .HasForeignKey(d => d.DebtorStatusID);

        }
    }
}
