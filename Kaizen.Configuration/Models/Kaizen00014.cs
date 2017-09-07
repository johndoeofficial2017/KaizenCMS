using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00014
    {
        public int ConditionID { get; set; }
        public int ViewID { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string FieldCondition { get; set; }
        public Nullable<bool> FieldOperatorAnd { get; set; }
        public virtual Kaizen00011 Kaizen00011 { get; set; }
    }
}
