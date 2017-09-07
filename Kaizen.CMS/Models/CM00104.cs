using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00104
    {
        public string AddressCode { get; set; }
        public string DebtorID { get; set; }
        public string AddressName { get; set; }
        public Nullable<bool> IsReadOnlyData { get; set; }
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
        public virtual CM00009 CM00009 { get; set; }
        public virtual CM00100 CM00100 { get; set; }
    }
}
