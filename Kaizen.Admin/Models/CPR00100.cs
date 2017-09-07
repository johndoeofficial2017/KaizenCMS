using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class CPR00100
    {
        public string DebtorID { get; set; }
        public Nullable<int> RecordTypeID { get; set; }
        public Nullable<byte> GenderID { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string PLACE_OF_BIRTH { get; set; }
        public string COUNTRY_OF_BIRTH { get; set; }
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
        public string AddressCode { get; set; }
        public string AddressEnglish { get; set; }
        public string AddressArabic { get; set; }
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
        public string PassportExpiryDate { get; set; }
        public string PassportIssueDate { get; set; }
        public string PassportNumber { get; set; }
        public string PassportSequnceNo { get; set; }
        public string PassportType { get; set; }
        public string VisaNo { get; set; }
        public string VisaExpiryDate { get; set; }
        public string VisaType { get; set; }
        public string ResidentPermitNo { get; set; }
        public string ResidentPermitExpiryDate { get; set; }
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
        public virtual CPR00001 CPR00001 { get; set; }
        public virtual CPR00101 CPR00101 { get; set; }
    }
}
