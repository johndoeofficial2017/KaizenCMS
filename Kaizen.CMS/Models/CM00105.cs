using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00105
    {
        public CM00105()
        {
            this.CM00109 = new List<CM00109>();
            this.CM00203 = new List<CM00203>();
            this.CM00220 = new List<CM00220>();
            this.CM10110 = new List<CM10110>();
        }

        public string AgentID { get; set; }
        public Nullable<int> CalendarID { get; set; }
        public string TargetID { get; set; }
        public Nullable<bool> IsPercentTarget { get; set; }
        public Nullable<int> TargetTypeID { get; set; }
        public string AgentGroup { get; set; }
        public string SupervisorID { get; set; }
        public string SuffixID { get; set; }
        public string MidName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<byte> GenderID { get; set; }
        public bool Inactive { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneCodeArea { get; set; }
        public string PhoneExtension { get; set; }
        public string DirectPhon { get; set; }
        public string SignatureExtension { get; set; }
        public string PhotoExtension { get; set; }
        public string UserCode { get; set; }
        public string WhereCondition { get; set; }
        public string AgentColor { get; set; }
        public virtual CM00005 CM00005 { get; set; }
        public virtual CM00023 CM00023 { get; set; }
        public virtual ICollection<CM00109> CM00109 { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
        public virtual ICollection<CM00220> CM00220 { get; set; }
        public virtual ICollection<CM10110> CM10110 { get; set; }
    }
}
