using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00043
    {
        public int RuleConditionID { get; set; }
        public string RuleConditionName { get; set; }
        public int CaseStatusID { get; set; }
        public string FieldID { get; set; }
        public Nullable<int> OperatorID { get; set; }
        public string FieldValues { get; set; }
        public string ParentFieldID { get; set; }
        public Nullable<int> ParentOperatorID { get; set; }
        public virtual CM00001 CM00001 { get; set; }
    }
}
