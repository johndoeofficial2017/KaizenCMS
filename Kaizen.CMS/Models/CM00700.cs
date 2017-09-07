using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00700
    {
        public CM00700()
        {
            this.CM00025 = new List<CM00025>();
            this.CM00026 = new List<CM00026>();
            this.CM00051 = new List<CM00051>();
            this.CM00052 = new List<CM00052>();
            this.CM00060 = new List<CM00060>();
            this.CM00062 = new List<CM00062>();
            this.CM00203 = new List<CM00203>();
        }

        public int CaseStatusID { get; set; }
        public Nullable<int> CaseStatusParent { get; set; }
        public int StatusActionTypeID { get; set; }
        public string CaseStatusname { get; set; }
        public Nullable<int> RequiredDays { get; set; }
        public bool IsHasChild { get; set; }
        public bool IsArchive { get; set; }
        public int CaseStatusTypeID { get; set; }
        public bool IsPTP { get; set; }
        public bool IsPTPRequired { get; set; }
        public bool IsPaymentApply { get; set; }
        public bool IsCloseReminder { get; set; }
        public bool IsReminder { get; set; }
        public bool IsTaskList { get; set; }
        public bool IsScripting { get; set; }
        public string RuleCondition { get; set; }
        public string StatusHTML { get; set; }
        public virtual CM00003 CM00003 { get; set; }
        public virtual ICollection<CM00025> CM00025 { get; set; }
        public virtual ICollection<CM00026> CM00026 { get; set; }
        public virtual ICollection<CM00051> CM00051 { get; set; }
        public virtual ICollection<CM00052> CM00052 { get; set; }
        public virtual CM00059 CM00059 { get; set; }
        public virtual ICollection<CM00060> CM00060 { get; set; }
        public virtual ICollection<CM00062> CM00062 { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
    }
}
