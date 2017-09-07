using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00100
    {
        public CM00100()
        {
            this.CM00101 = new List<CM00101>();
            this.CM00104 = new List<CM00104>();
            this.CM00106 = new List<CM00106>();
            this.CM00118 = new List<CM00118>();
            this.CM00119 = new List<CM00119>();
            this.CM00203 = new List<CM00203>();
            this.CM10201 = new List<CM10201>();
        }

        public string DebtorID { get; set; }
        public Nullable<byte> GenderID { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string PLACE_OF_BIRTH { get; set; }
        public string COUNTRY_OF_BIRTH { get; set; }
        public Nullable<bool> IsJoint { get; set; }
        public string MaritalID { get; set; }
        public string DebtorDescription { get; set; }
        public string FirstNameEnglish { get; set; }
        public string FirstNameArabic { get; set; }
        public string EnglishFullName { get; set; }
        public string ArabicFullName { get; set; }
        public string SHORT_NAME_ARAB { get; set; }
        public string SHORT_NAME_ENG { get; set; }
        public string MiddleName1English { get; set; }
        public string MiddleName2English { get; set; }
        public string MiddleName3English { get; set; }
        public string MiddleName4English { get; set; }
        public string MiddleName1Arabic { get; set; }
        public string MiddleName2Arabic { get; set; }
        public string MiddleName3Arabic { get; set; }
        public string MOTHER_FIRST_NAME { get; set; }
        public string MOTHER_LAST_NAME { get; set; }
        public Nullable<int> ContactTypeID { get; set; }
        public string ResidenceNo { get; set; }
        public string GovernorateNo { get; set; }
        public string GovernorateNameEnglish { get; set; }
        public string EmployerFlag1 { get; set; }
        public string EmployerName1 { get; set; }
        public string EmployerNo1 { get; set; }
        public string EmployerNameArabic { get; set; }
        public string EmploymentFlag { get; set; }
        public string LaborForceParticipation { get; set; }
        public string LatestEducationDegree { get; set; }
        public string EmploymentNameEnglish { get; set; }
        public string OccupationDescription1 { get; set; }
        public string OccupationEnglish { get; set; }
        public string SponsorCPRNoorUnitNo { get; set; }
        public string SponsorFlag { get; set; }
        public string SponsorName { get; set; }
        public string SponserId { get; set; }
        public string SponserNameEnglish { get; set; }
        public string FingerprintCode { get; set; }
        public string IdNumber { get; set; }
        public string ParentDebtor { get; set; }
        public string ReligionID { get; set; }
        public string CUSTCLAS { get; set; }
        public Nullable<int> GroupID { get; set; }
        public Nullable<int> PriorityID { get; set; }
        public Nullable<int> DebtorStatusID { get; set; }
        public Nullable<bool> IsHold { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string PhotoExtension { get; set; }
        public string SignatureExtension { get; set; }
        public string CPRExpiryDate { get; set; }
        public string CPRIssueDate { get; set; }
        public string CPRSerialNumber { get; set; }
        public string CPRCRNo { get; set; }
        public Nullable<System.DateTime> PassportExpiryDate { get; set; }
        public Nullable<System.DateTime> PassportIssueDate { get; set; }
        public string PassportNumber { get; set; }
        public string PassportSequnceNo { get; set; }
        public string PassportType { get; set; }
        public string VisaNo { get; set; }
        public Nullable<System.DateTime> VisaExpiryDate { get; set; }
        public string VisaType { get; set; }
        public string ResidentPermitNo { get; set; }
        public Nullable<System.DateTime> ResidentPermitExpiryDate { get; set; }
        public string TypeOfResident { get; set; }
        public string NationalityID { get; set; }
        public Nullable<int> NO_OF_DEPENDENTS { get; set; }
        public Nullable<double> MonthlySalary { get; set; }
        public Nullable<double> AMT01 { get; set; }
        public Nullable<double> AMT02 { get; set; }
        public Nullable<double> AMT03 { get; set; }
        public string TxtField01 { get; set; }
        public string TxtField02 { get; set; }
        public string TxtField03 { get; set; }
        public string TxtField04 { get; set; }
        public Nullable<bool> CheckBoxField01 { get; set; }
        public Nullable<bool> CheckBoxField02 { get; set; }
        public Nullable<bool> CheckBoxField03 { get; set; }
        public Nullable<bool> CheckBoxField04 { get; set; }
        public Nullable<System.DateTime> Date01 { get; set; }
        public Nullable<System.DateTime> Date02 { get; set; }
        public Nullable<System.DateTime> Date03 { get; set; }
        public Nullable<System.DateTime> Date04 { get; set; }
        public string AddressCode { get; set; }
        public string AddressEnglish { get; set; }
        public string AddressArabic { get; set; }
        public string AddressName { get; set; }
        public string WebSite { get; set; }
        public string CountryID { get; set; }
        public string CityID { get; set; }
        public string Phone01 { get; set; }
        public string Phone02 { get; set; }
        public string Phone03 { get; set; }
        public string Phone04 { get; set; }
        public string Ext1 { get; set; }
        public string Ext2 { get; set; }
        public string Ext3 { get; set; }
        public string Ext4 { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string MobileNo3 { get; set; }
        public string MobileNo4 { get; set; }
        public string POBox { get; set; }
        public string Other01 { get; set; }
        public string Other02 { get; set; }
        public string Other03 { get; set; }
        public string Other04 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Email01 { get; set; }
        public string Email02 { get; set; }
        public string Email03 { get; set; }
        public string Email04 { get; set; }
        public string FlatNo { get; set; }
        public string BuildingNo { get; set; }
        public string RoadName { get; set; }
        public string RoadNo { get; set; }
        public string BlockNo { get; set; }
        public string BlockName { get; set; }
        public virtual CM00010 CM00010 { get; set; }
        public virtual CM00011 CM00011 { get; set; }
        public virtual CM00012 CM00012 { get; set; }
        public virtual CM00014 CM00014 { get; set; }
        public virtual ICollection<CM00101> CM00101 { get; set; }
        public virtual CM00102 CM00102 { get; set; }
        public virtual ICollection<CM00104> CM00104 { get; set; }
        public virtual ICollection<CM00106> CM00106 { get; set; }
        public virtual ICollection<CM00118> CM00118 { get; set; }
        public virtual ICollection<CM00119> CM00119 { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
        public virtual ICollection<CM10201> CM10201 { get; set; }
    }
}
