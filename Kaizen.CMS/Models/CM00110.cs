using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00110
    {
        public CM00110()
        {
            this.CM00111 = new List<CM00111>();
            this.CM00112 = new List<CM00112>();
            this.CM00113 = new List<CM00113>();
            this.CM00115 = new List<CM00115>();
            this.CM00120 = new List<CM00120>();
            this.CM00200 = new List<CM00200>();
        }

        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string ShortName { get; set; }
        public string ClientDescription { get; set; }
        public string NationaltyID { get; set; }
        public string NationalityName { get; set; }
        public Nullable<bool> IsJoint { get; set; }
        public string ParentClientID { get; set; }
        public string ExtraField { get; set; }
        public string CUSTCLAS { get; set; }
        public string StatusID { get; set; }
        public string PhotoExtension { get; set; }
        public Nullable<int> ContactTypeID { get; set; }
        public Nullable<int> AddressCode { get; set; }
        public Nullable<int> BillAddressCode { get; set; }
        public Nullable<int> RemitToAddressCode { get; set; }
        public virtual CM00021 CM00021 { get; set; }
        public virtual CM00022 CM00022 { get; set; }
        public virtual ICollection<CM00111> CM00111 { get; set; }
        public virtual ICollection<CM00112> CM00112 { get; set; }
        public virtual ICollection<CM00113> CM00113 { get; set; }
        public virtual ICollection<CM00115> CM00115 { get; set; }
        public virtual ICollection<CM00120> CM00120 { get; set; }
        public virtual ICollection<CM00200> CM00200 { get; set; }
    }
}
