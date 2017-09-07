using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM20210
    {
        public int TrxID { get; set; }
        public string CaseRef { get; set; }
        public string TRXTypeID { get; set; }
        public string TrxTypeName { get; set; }
        public string ContractID { get; set; }
        public string ContractName { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string BatchID { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<byte> DecimalDigit { get; set; }
        public Nullable<bool> IsMultiply { get; set; }
        public Nullable<double> ExchangeRate { get; set; }
        public string CIFNumber { get; set; }
        public string CIFName { get; set; }
        public Nullable<bool> IsJoint { get; set; }
        public Nullable<bool> IsHasHistory { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string AddressCode { get; set; }
        public string AddressName { get; set; }
        public string CaseAddess { get; set; }
        public int CaseStatusID { get; set; }
        public Nullable<int> CaseStatusChild { get; set; }
        public string CaseStatusname { get; set; }
        public string CaseStatusChildName { get; set; }
        public string CaseStatusComment { get; set; }
        public Nullable<System.DateTime> ReminderDateTime { get; set; }
        public Nullable<double> PTPAMT { get; set; }
        public Nullable<int> CYCLEDAY { get; set; }
        public Nullable<int> GRADE { get; set; }
        public Nullable<int> BucketPrev { get; set; }
        public Nullable<int> BucketID { get; set; }
        public string TxtField01 { get; set; }
        public string TxtField02 { get; set; }
        public string TxtField03 { get; set; }
        public Nullable<double> OutstandingAMT { get; set; }
        public Nullable<double> OSAmount { get; set; }
        public Nullable<double> FinanceCharge { get; set; }
        public double ClaimAmount { get; set; }
        public Nullable<double> WriteOffAMT { get; set; }
        public Nullable<double> PrincipleAmount { get; set; }
        public Nullable<double> CreditLimit { get; set; }
        public Nullable<double> LastPaymentAMTUpload { get; set; }
        public Nullable<double> TotalLifeCollactedAMT { get; set; }
        public Nullable<double> AMT01 { get; set; }
        public Nullable<double> AMT02 { get; set; }
        public Nullable<double> AMT03 { get; set; }
        public Nullable<int> ContactTypeID { get; set; }
        public string CaseAccountNo { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<int> OverDueDays { get; set; }
        public Nullable<System.DateTime> OverDueDate { get; set; }
        public Nullable<System.DateTime> FirstDisburementDate { get; set; }
        public Nullable<System.DateTime> LastPaymentDateUplod { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public System.DateTime BookingDate { get; set; }
        public Nullable<System.DateTime> FirstLifeOverDueDate { get; set; }
        public Nullable<System.DateTime> Date01 { get; set; }
        public Nullable<System.DateTime> Date02 { get; set; }
        public Nullable<System.DateTime> Date03 { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string AgentID { get; set; }
        public System.DateTime AssignDate { get; set; }
        public Nullable<System.DateTime> LastPaymentDate { get; set; }
        public Nullable<double> LastCallactedAMT { get; set; }
        public Nullable<double> TotalCallactedAMT { get; set; }
        public string LastPaymentBy { get; set; }
        public string ContactNo { get; set; }
        public string WebSite { get; set; }
        public string CountryID { get; set; }
        public string CityID { get; set; }
        public string Phone01 { get; set; }
        public string Phone02 { get; set; }
        public string Ext1 { get; set; }
        public string Ext2 { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string POBox { get; set; }
        public string Other01 { get; set; }
        public string Other02 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Email01 { get; set; }
        public string Email02 { get; set; }
    }
}
