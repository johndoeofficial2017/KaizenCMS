using System;
using System.Collections.Generic;

namespace Kaizen.SOP
{
    public partial class SOP00100
    {
        public SOP00100()
        {
            this.IV00108 = new List<IV00108>();
            this.SOP00200 = new List<SOP00200>();
            this.SOP00101 = new List<SOP00101>();
            this.SOP00105 = new List<SOP00105>();
            this.SOP00106 = new List<SOP00106>();
            this.SOP10100 = new List<SOP10100>();
        }

        public string CUSTNMBR { get; set; }
        public string GroupID { get; set; }
        public Nullable<int> StatementCycleID { get; set; }
        public string CurrencyCode { get; set; }
        public string PriorityID { get; set; }
        public string CUSTNAME { get; set; }
        public string ShortName { get; set; }
        public string CUSTCLAS { get; set; }
        public Nullable<int> StatusID { get; set; }
        public bool IsHold { get; set; }
        public bool IsActive { get; set; }
        public string CustomerDescription { get; set; }
        public string PhotoExtension { get; set; }
        public string ParentCustomer { get; set; }
        public string CPRCRNo { get; set; }
        public string EmployerName { get; set; }
        public string NationalityID { get; set; }
        public string ShipTo { get; set; }
        public string BillTo { get; set; }
        public string StatementTo { get; set; }
        public string AddressTypeCode { get; set; }
        public string ContactTypeID { get; set; }
        public Nullable<byte> PriceLevelCode { get; set; }
        public string SalePersonID { get; set; }
        public string UserCode { get; set; }
        public System.DateTime EntryDate { get; set; }
        public int KaizenID { get; set; }
        public virtual ICollection<IV00108> IV00108 { get; set; }
        public virtual SOP00010 SOP00010 { get; set; }
        public virtual SOP00011 SOP00011 { get; set; }
        public virtual SOP00012 SOP00012 { get; set; }
        public virtual SOP00013 SOP00013 { get; set; }
        public virtual SOP00014 SOP00014 { get; set; }
        public virtual SOP00102 SOP00102 { get; set; }
        public virtual ICollection<SOP00200> SOP00200 { get; set; }
        public virtual Sys00030 Sys00030 { get; set; }
        public virtual ICollection<SOP00101> SOP00101 { get; set; }
        public virtual SOP00103 SOP00103 { get; set; }
        public virtual ICollection<SOP00105> SOP00105 { get; set; }
        public virtual ICollection<SOP00106> SOP00106 { get; set; }
        public virtual ICollection<SOP10100> SOP10100 { get; set; }
    }
}
