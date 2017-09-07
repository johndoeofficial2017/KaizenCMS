using System;
using System.Collections.Generic;

namespace Kaizen.Admin
{
    public partial class CPR00101
    {
        public string DebtorID { get; set; }
        public string AddressArabic { get; set; }
        public string FirstNameArabic { get; set; }
        public string LastNameArabic { get; set; }
        public string MiddleName1Arabic { get; set; }
        public string MiddleName2Arabic { get; set; }
        public string MiddleName3Arabic { get; set; }
        public string MiddleName4Arabic { get; set; }
        public string BloodGroup { get; set; }
        public string BuildingAlphaArabic { get; set; }
        public string EmployerName1Arabic { get; set; }
        public string OccupationArabic { get; set; }
        public string SponserNameArabic { get; set; }
        public string RoadNameArabic { get; set; }
        public string BlockNameArabic { get; set; }
        public string GovernorateNameArabic { get; set; }
        public string LatestEducationDegreeArabic { get; set; }
        public string OccupationDescription1Arabic { get; set; }
        public string SponsorNameArabic { get; set; }
        public string ClearingAgentIndicator { get; set; }
        public string LfpNameArabic { get; set; }
        public string ArabicCountryName { get; set; }
        public virtual CPR00100 CPR00100 { get; set; }
    }
}
