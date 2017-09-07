using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00110
    {
        public string SalePersonID { get; set; }
        public string UserCode { get; set; }
        public string EmployeeID { get; set; }
        public string SupervisorID { get; set; }
        public int SalePersonTypeID { get; set; }
        public string MidName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Inactive { get; set; }
        public string EmailAddress { get; set; }
        public string DirectPhon { get; set; }
        public string PhonExtension { get; set; }
        public string SiteID { get; set; }
        public Nullable<bool> BinTrack { get; set; }
        public string SubBinID { get; set; }
        public Nullable<int> BinID { get; set; }
        public string SOPCUSTNMBR { get; set; }
        public virtual SOP00008 SOP00008 { get; set; }
    }
}
