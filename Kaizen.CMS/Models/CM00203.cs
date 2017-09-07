using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00203
    {
        public CM00203()
        {
            this.CM00204 = new List<CM00204>();
            this.CM00205 = new List<CM00205>();
            this.CM00213 = new List<CM00213>();
            this.CM10207 = new List<CM10207>();
            this.CM10307 = new List<CM10307>();
            this.CM10701 = new List<CM10701>();
        }

        public string CaseRef { get; set; }
        public int TRXTypeID { get; set; }
        public Nullable<bool> IsHold { get; set; }
        public Nullable<int> PstDueCountOpen { get; set; }
        public Nullable<int> PstDueCountClient { get; set; }
        public Nullable<int> PstDueCountHstry { get; set; }
        public string ContractID { get; set; }
        public string ContractName { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string NationaltyClient { get; set; }
        public string NationltyCreditor { get; set; }
        public string NationltyPartner { get; set; }
        public string NationaltyIDCIF { get; set; }
        public string NationalityName { get; set; }
        public string EmployerName { get; set; }
        public string CreditorID { get; set; }
        public string CreditorName { get; set; }
        public string PartnerID { get; set; }
        public string PartnerName { get; set; }
        public Nullable<System.DateTime> PartnrAssinDate { get; set; }
        public string LegalID { get; set; }
        public string LegalName { get; set; }
        public Nullable<System.DateTime> LegalAssignDate { get; set; }
        public string BatchID { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<byte> DecimalDigit { get; set; }
        public Nullable<bool> IsMultiply { get; set; }
        public Nullable<double> ExchangeRate { get; set; }
        public string CIFNumber { get; set; }
        public string CPRCRNo { get; set; }
        public string CIFName { get; set; }
        public Nullable<int> PriorityID { get; set; }
        public string PriorityName { get; set; }
        public Nullable<double> NPA { get; set; }
        public Nullable<double> TaskComplated { get; set; }
        public Nullable<int> ContactTypeID { get; set; }
        public Nullable<bool> IsJoint { get; set; }
        public int CaseStatusID { get; set; }
        public Nullable<int> CaseStatusChild { get; set; }
        public string CaseStatusname { get; set; }
        public string CaseStatusChildName { get; set; }
        public string CaseStatusComment { get; set; }
        public Nullable<System.DateTime> ReminderDateTime { get; set; }
        public Nullable<double> PTPAMT { get; set; }
        public Nullable<int> PTPCount { get; set; }
        public Nullable<int> PTPBroken { get; set; }
        public Nullable<int> PTPkept { get; set; }
        public Nullable<int> LastCaseStatusID { get; set; }
        public Nullable<int> LastCasStatusChldID { get; set; }
        public string LastCasStatusname { get; set; }
        public string LastCasStatusChldNam { get; set; }
        public string LastCasStatusComment { get; set; }
        public Nullable<int> MidCasStatusID { get; set; }
        public Nullable<int> MidCasStatusChld { get; set; }
        public string MidCasStatusnam { get; set; }
        public string MidCasStatusChldName { get; set; }
        public string MidCasStatusComment { get; set; }
        public Nullable<int> CYCLEDAY { get; set; }
        public Nullable<int> BucketPrev { get; set; }
        public string BucketPrevName { get; set; }
        public Nullable<int> BucketID { get; set; }
        public string BucketName { get; set; }
        public Nullable<int> Lookup01 { get; set; }
        public string Lookup01Name { get; set; }
        public Nullable<int> Lookup02 { get; set; }
        public string Lookup02Name { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ProductName { get; set; }
        public string TxtField01 { get; set; }
        public string TxtField02 { get; set; }
        public string TxtField03 { get; set; }
        public string TxtField04 { get; set; }
        public string TxtField05 { get; set; }
        public string TxtField06 { get; set; }
        public string TxtField07 { get; set; }
        public Nullable<bool> CheckBoxField02 { get; set; }
        public Nullable<bool> CheckBoxField01 { get; set; }
        public Nullable<bool> CheckBoxField03 { get; set; }
        public Nullable<double> AMT01 { get; set; }
        public Nullable<double> AMT02 { get; set; }
        public Nullable<double> AMT03 { get; set; }
        public Nullable<double> AMT04 { get; set; }
        public Nullable<double> AMT05 { get; set; }
        public Nullable<double> AMT06 { get; set; }
        public Nullable<double> AMT07 { get; set; }
        public Nullable<double> AMT08 { get; set; }
        public Nullable<double> AMT09 { get; set; }
        public Nullable<double> AMT10 { get; set; }
        public Nullable<System.DateTime> Date01 { get; set; }
        public Nullable<System.DateTime> Date02 { get; set; }
        public Nullable<System.DateTime> Date03 { get; set; }
        public Nullable<System.DateTime> Date04 { get; set; }
        public Nullable<double> PrincipleAmount { get; set; }
        public Nullable<double> MaturityAmount { get; set; }
        public Nullable<double> OutstandingAMT { get; set; }
        public Nullable<double> OutStandingToday { get; set; }
        public Nullable<double> InstallmentAmount { get; set; }
        public double ClaimAmount { get; set; }
        public Nullable<double> FinanceCharge { get; set; }
        public Nullable<double> WriteOffAMT { get; set; }
        public Nullable<double> CreditLimit { get; set; }
        public Nullable<double> LastPaymentAMTUpload { get; set; }
        public Nullable<double> TotalLifeCollactedAMT { get; set; }
        public string CaseAccountNo { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<int> OverDueDays { get; set; }
        public Nullable<System.DateTime> FirstDisburementDate { get; set; }
        public Nullable<System.DateTime> MATURITY_DATE { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> OverDueDate { get; set; }
        public Nullable<System.DateTime> LastPaymentDateUplod { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public System.DateTime BookingDate { get; set; }
        public Nullable<System.DateTime> FirstLifeOverDueDate { get; set; }
        public string Remarks { get; set; }
        public string AgentID { get; set; }
        public Nullable<System.DateTime> AssignDate { get; set; }
        public Nullable<System.DateTime> LastPaymentDate { get; set; }
        public Nullable<double> LastCallactedAMT { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
        public Nullable<double> CommissionRate { get; set; }
        public string LastPaymentBy { get; set; }
        public Nullable<double> MonthlySalary { get; set; }
        public string AddressCode { get; set; }
        public string CaseAddess { get; set; }
        public string Phone01 { get; set; }
        public string Phone02 { get; set; }
        public string Phone03 { get; set; }
        public string Phone04 { get; set; }
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
        public virtual CM00015 CM00015 { get; set; }
        public virtual CM00016 CM00016 { get; set; }
        public virtual CM00017 CM00017 { get; set; }
        public virtual CM00029 CM00029 { get; set; }
        public virtual CM00055 CM00055 { get; set; }
        public virtual CM00100 CM00100 { get; set; }
        public virtual CM00105 CM00105 { get; set; }
        public virtual CM00130 CM00130 { get; set; }
        public virtual CM00140 CM00140 { get; set; }
        public virtual CM00170 CM00170 { get; set; }
        public virtual CM00700 CM00700 { get; set; }
        public virtual CM10100 CM10100 { get; set; }
        public virtual ICollection<CM00204> CM00204 { get; set; }
        public virtual ICollection<CM00205> CM00205 { get; set; }
        public virtual ICollection<CM00213> CM00213 { get; set; }
        public virtual ICollection<CM10207> CM10207 { get; set; }
        public virtual ICollection<CM10307> CM10307 { get; set; }
        public virtual ICollection<CM10701> CM10701 { get; set; }
    }
}
