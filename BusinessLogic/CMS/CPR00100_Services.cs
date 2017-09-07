using System;
using System.Collections.Generic;
using Kaizen.Admin.Repository;
using Kaizen.Admin;

namespace Kaizen.BusinessLogic.Admin
{
    public class CPR00100Services
    {
        #region Infrastructure

        private Kaizen.Admin.Repository.CPR00100Repository _CPR00100Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CPR00100Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CPR00100Repository = new CPR00100Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CPR00100> GetWhereListWithPaging(string Filters, string Searchcritaria, string ViewWhereCondition, string ViewSortCondition, int PageSize, int Page, string Member, bool IsAscending)
        {
            //if (!string.IsNullOrEmpty(Searchcritaria) && !string.IsNullOrEmpty(Filters))
            //    Filters += " and ";
            //if (!string.IsNullOrEmpty(Searchcritaria) && string.IsNullOrEmpty(Filters))
            //    Filters += Searchcritaria;

            if (!string.IsNullOrEmpty(Filters) && !string.IsNullOrEmpty(ViewWhereCondition))
                Filters += " and ";
            Filters += ViewWhereCondition;
            //string SortDirection = string.Empty;
            //if (IsAscending)
            //    SortDirection = " ORDER BY " + Member + " asc";
            //else
            //    SortDirection = " ORDER BY " + Member + " desc";
            //if (!string.IsNullOrEmpty(ViewSortCondition))
            //    ViewSortCondition = SortDirection + "," + ViewSortCondition;
            //else
            //    ViewSortCondition = SortDirection;
            DataCollection<CPR00100> result = null;
            result = _CPR00100Repository.GetWhereListWithPaging("CPR00100", PageSize, Page, Filters, Member, IsAscending);
            return result;
        }

//        public DataCollection<CPR00100View> GetBYSQLQuery(string Filters,
//string FieldID, int FltrOperator, string Searchcritaria,
//int PageSize, int Page, string Member, bool IsAscending)
//        {
//            string SeachStr = string.Empty;
//            if (string.IsNullOrEmpty(Searchcritaria))
//            {
//                if (!string.IsNullOrEmpty(Filters))
//                    SeachStr = Filters;
//            }
//            else
//            {
//                if (!string.IsNullOrEmpty(Filters))
//                    SeachStr = " and ";
//                if (FieldID == "-1")
//                {
//                    SeachStr += Help.GetFilter("DebtorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("GenderID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("BirthDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("DebtorDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("AddressEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("FirstNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("EnglishFullName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("LastNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("MiddleName1English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("MiddleName2English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("MiddleName3English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("MiddleName4English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("ContactNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("Email", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("ResidenceNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("FlatNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("BuildingNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("BuildingAlpha", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("RoadName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("RoadNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("BlockNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("BlockName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("GovernorateNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("GovernorateNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("EmployerFlag1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("EmployerName1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("EmployerNo1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("EmploymentFlag1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("LaborForceParticipation", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("LatestEducationDegree", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("EmploymentNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("OccupationDescription1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("SponsorCPRNoorUnitNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("SponsorFlag", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("SponsorName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("SponserId", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("SponserNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("LfpNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("EnglishCountryName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("IACOCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("FingerprintCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("IdNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("OccupationEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("ParentDebtor", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("CUSTCLAS", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("GroupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("PriorityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("DebtorStatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("IsHold", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("IsActive", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("CPRExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("CPRIssueDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("CPRSerialNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("CPRCRNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("PassportExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("PassportIssueDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("PassportNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("PassportSequnceNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("PassportType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("VisaNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("VisaExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("VisaType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("ResidentPermitNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("ResidentPermitExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("TypeOfResident", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("CardVerificationStatus", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                    SeachStr += " or ";
//                    SeachStr += Help.GetFilter("NationalityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
//                }
//                else
//                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
//            }

//            CPR00100ViewRepository rep = new CPR00100ViewRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
//            DataCollection<CPR00100View> result = null;
//            if (string.IsNullOrEmpty(SeachStr))
//                result = rep.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
//            else
//                result = rep.GetWhereListWithPaging("CPR00100View", PageSize, Page, SeachStr, Member, IsAscending);
//            return result;
//        }
        public DataCollection<CPR00100> GetViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            else
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = " and ";
                if (FieldID == "-1")
                {
                    SeachStr += Help.GetFilter("DebtorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GenderID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BirthDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DebtorDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FirstNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EnglishFullName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LastNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName1English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName2English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName3English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName4English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Email", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ResidenceNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FlatNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BuildingNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BuildingAlpha", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RoadName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RoadNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BlockNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BlockName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GovernorateNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GovernorateNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployerFlag1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployerName1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployerNo1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmploymentFlag1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LaborForceParticipation", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LatestEducationDegree", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmploymentNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OccupationDescription1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponsorCPRNoorUnitNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponsorFlag", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponsorName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponserId", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponserNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LfpNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EnglishCountryName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IACOCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FingerprintCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IdNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OccupationEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ParentDebtor", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTCLAS", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GroupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PriorityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DebtorStatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsHold", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsActive", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRIssueDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRSerialNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRCRNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportIssueDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportSequnceNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VisaNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VisaExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VisaType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ResidentPermitNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ResidentPermitExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TypeOfResident", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CardVerificationStatus", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("NationalityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }
            DataCollection<CPR00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CPR00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CPR00100Repository.GetWhereListWithPaging("CPR00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CPR00100> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            else
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = " and ";
                if (FieldID == "-1")
                {
                    SeachStr += Help.GetFilter("DebtorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GenderID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BirthDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DebtorDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FirstNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EnglishFullName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LastNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName1English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName2English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName3English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName4English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Email", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ResidenceNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FlatNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BuildingNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BuildingAlpha", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RoadName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RoadNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BlockNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BlockName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GovernorateNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GovernorateNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployerFlag1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployerName1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployerNo1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmploymentFlag1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LaborForceParticipation", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LatestEducationDegree", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmploymentNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OccupationDescription1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponsorCPRNoorUnitNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponsorFlag", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponsorName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponserId", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponserNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LfpNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EnglishCountryName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IACOCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FingerprintCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IdNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OccupationEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ParentDebtor", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTCLAS", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GroupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PriorityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DebtorStatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsHold", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsActive", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRIssueDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRSerialNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRCRNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportIssueDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportSequnceNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VisaNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VisaExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VisaType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ResidentPermitNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ResidentPermitExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TypeOfResident", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CardVerificationStatus", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("NationalityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }
            DataCollection<CPR00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CPR00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CPR00100Repository.GetWhereListWithPaging("CPR00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CPR00100> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string DebtorID,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            else
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = " and ";
                if (FieldID == "-1")
                {
                    SeachStr += Help.GetFilter("DebtorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GenderID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BirthDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DebtorDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FirstNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EnglishFullName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LastNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName1English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName2English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName3English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("MiddleName4English", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("Email", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ResidenceNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FlatNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BuildingNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BuildingAlpha", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RoadName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RoadNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BlockNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BlockName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GovernorateNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GovernorateNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployerFlag1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployerName1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmployerNo1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmploymentFlag1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LaborForceParticipation", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LatestEducationDegree", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EmploymentNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OccupationDescription1", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponsorCPRNoorUnitNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponsorFlag", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponsorName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponserId", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("SponserNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LfpNameEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("EnglishCountryName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IACOCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FingerprintCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IdNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("OccupationEnglish", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ParentDebtor", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTCLAS", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("GroupID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PriorityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("DebtorStatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsHold", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("IsActive", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRIssueDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRSerialNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CPRCRNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportIssueDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportNumber", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportSequnceNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("PassportType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VisaNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VisaExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VisaType", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ResidentPermitNo", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ResidentPermitExpiryDate", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TypeOfResident", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CardVerificationStatus", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("NationalityID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CPR00100> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CPR00100Repository.GetListWithPaging(ss => ss.ParentDebtor.Trim() == DebtorID.Trim(), PageSize, Page, ss => Member);
            else
                result = _CPR00100Repository.GetWhereListWithPaging("CPR00100", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CPR00100> SearchDebtorData(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            else
            {
                SeachStr = Searchcritaria;
            }
            string str = string.Empty;

            //DataCollection<CM00104> result = null;
            DataCollection<CPR00100> CPR00100Result = null;

            //CM00104Repository _CM00104Repository = new CM00104Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            if (string.IsNullOrEmpty(SeachStr))
            {
                CPR00100Result = _CPR00100Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                //result = _CM00104Repository.GetListWithPaging(ss => ss.Phone01.Contains(SeachStr) || ss.Phone02.Contains(SeachStr)
                    //|| ss.Phone03.Contains(SeachStr) || ss.Phone02.Contains(SeachStr) || ss.DebtorID.Contains(SeachStr), PageSize, Page, ss => Member);
                //if (result.Items != null)
                //{
                    //foreach (var CPR00100 in result.Items)
                    //{
                    //    if (CPR00100 != null)
                    //    {
                    //        if (CPR00100.DebtorID != null)
                    //        {
                    //            if (string.IsNullOrEmpty(str))
                    //                str += CPR00100.DebtorID.ToString();
                    //            else
                    //                str += "," + CPR00100.DebtorID.ToString();
                    //        }
                    //    }
                    //}
                    //string sql = "select * from CPR00100 where DebtorID in(" + str.Trim() + ")";
                    //CPR00100Result = _CPR00100Repository.GetSQLData(sql);
                //}
                //else
                //{
                    CPR00100Result = _CPR00100Repository.GetListWithPaging(ss => ss.DebtorID.Contains(SeachStr), PageSize, Page, ss => Member);
                //}
            }
            return CPR00100Result;
        }

        public DataCollection<CPR00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<CPR00100> L = null;
            var tasks = _CPR00100Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<CPR00100> result = tasks;
            return result;
        }
        public DataCollection<CPR00100> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<CPR00100> result = null;
            var tasks = _CPR00100Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }


        public DataCollection<CPR00100> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CPR00100Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CPR00100> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CPR00100Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CPR00100> result = tasks;
                return result;
            }


        }
        //public DataCollection<CPR00100View> GetSQLData(string Searchcritaria, int ViewID, int PageSize, int Page, string Member, string SortDirection)
        //{
        //    //Kaizen.BusinessLogic.Configuration.Kaizen00011Services oKaizen00011ColumnServices = new Kaizen.BusinessLogic.Configuration.Kaizen00011Services(UserContext);
        //    //List<GridCPR00100> oGridCPR00100ColumnList = oKaizen00011ColumnServices.GetAllGridCPR00100(ViewID).ToList();
        //    //string fldlist = string.Empty;
        //    //foreach(GridCPR00100 fldid in oGridCPR00100ColumnList)
        //    //{
        //    //    fldlist += fldid.GridCM00101.ColumnName + ",";
        //    //}
        //    //fldlist = fldlist.Substring(0, fldlist.Length - 1);
        //    Page = Page - 1;
        //    DataCollection<CPR00100View> result = null;

        //    Kaizen.Admin.Repository.CPR00100ViewRepository _CPR00100ViewRepository = new CPR00100ViewRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
        //    string sql = "select * from CPR00100View " + Searchcritaria + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
        //    var tasks = _CPR00100ViewRepository.GetSQLData(sql);
        //    if (tasks != null)
        //    {
        //        if (string.IsNullOrEmpty(Searchcritaria))
        //            tasks.TotalItemCount = (int)_CPR00100ViewRepository.Count();
        //        else
        //            tasks.TotalItemCount = (int)_CPR00100ViewRepository.GetSQLINT("select count(DebtorID) from CPR00100View " + Searchcritaria);
        //        tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
        //    }
        //    result = tasks;
        //    return result;
        //}
        public IList<CPR00100> GetAll()
        {
            var tasks = _CPR00100Repository.GetAll();
            IList<CPR00100> result = tasks;
            return result;
        }
        public DataCollection<CPR00100> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member
            , string SortDirection)
        {
            DataCollection<CPR00100> result = null;

            var tasks = _CPR00100Repository.GetListWithPaging(SearchTerm,
                PageSize, Page, ss => ss.DebtorID, true);
            result = tasks;
            return result;
        }

        public CPR00100 GetSingle(string DebtorID)
        {
            var tasks = _CPR00100Repository.GetSingle(x => x.DebtorID == DebtorID);
            //if (!string.IsNullOrEmpty(tasks.AddressCode))
            //{
            //    CM00104Repository _CM00104Repository = new CM00104Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            //    tasks.MainAddress = _CM00104Repository.GetSingle(ss => ss.AddressCode == tasks.AddressCode && ss.DebtorID == DebtorID);
            //}
            //if (!string.IsNullOrEmpty(tasks.BillTo))
            //{
            //    CM00104Repository _CM00104Repository = new CM00104Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            //    tasks.BillAddress = _CM00104Repository.GetSingle(ss => ss.AddressCode == tasks.BillTo && ss.DebtorID == DebtorID);
            //}
            //if (!string.IsNullOrEmpty(tasks.StatementTo))
            //{
            //    CM00104Repository _CM00104Repository = new CM00104Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            //    tasks.StatementAddress = _CM00104Repository.GetSingle(ss => ss.AddressCode == tasks.StatementTo && ss.DebtorID == DebtorID);
            //}
            CPR00101Repository _CPR00101Repository = new CPR00101Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var CPR00101List = _CPR00101Repository.GetSingle(cc => cc.DebtorID == tasks.DebtorID);
            tasks.CPR00101 = CPR00101List;
            tasks.DebtorID = tasks.DebtorID.Trim();
            return tasks;
        }
        public IList<CPR00001> GetRecordTypes()
        {
            CPR00001Repository _CPR00101Repository = new CPR00001Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var CPR00001List = _CPR00101Repository.GetAll();
            return CPR00001List;
        }
        public DataCollection<CPR00100> GetTop10BYDebtorID(string DebtorID)
        {
            string SeachStr = string.Empty;
            SeachStr = "DebtorID Like '" + DebtorID.Trim() + "%'";
            var result = _CPR00100Repository.GetWhereListWithPaging("CPR00100", 10, 1, SeachStr, "DebtorID", true);
            return result;
        }

        public KaizenResult AddCPR00100(CPR00100 newTask)
        {
            newTask.DebtorID = newTask.DebtorID.Trim();
            //newTask.EnglishFullName = newTask.EnglishFirstName + " " + newTask.EnglishMiddleName2 + " " + newTask.EnglishMiddleName3 + " " + newTask.EnglishLastName;
            KaizenResult result = _CPR00100Repository.AddKaizenObject(newTask);
            return result;
        }
        
        public KaizenResult Update(CPR00100 newTask)
        {
            KaizenResult result = _CPR00100Repository.UpdateKaizenObject(newTask);
            _CPR00100Repository.UpdateKaizenObject(newTask);
            //if (newTask.CM00102 != null && !string.IsNullOrEmpty(newTask.CM00102.DebtorID))
            //{
            //    if (_CM00102Repository.GetSingle(cc => cc.DebtorID == newTask.DebtorID) != null)
            //    {

            //    }
            //    else
            //    {
            //        newTask.CM00102.CPR00100 = null;
            //        _CM00102Repository.AddKaizenObject(newTask.CM00102);
            //    }
            //}
            return result;
        }
        public KaizenResult Delete(string DebtorID)
        {
            KaizenResult result = _CPR00100Repository.RemoveKaizenObject(xx => xx.DebtorID.Trim() == DebtorID.Trim());
            return result;
        }

    }
}
