using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00015
    {
        public string CompanyID { get; set; }
        public string TRXTypeID { get; set; }
        public int ScreenID { get; set; }
        public string FieldName { get; set; }
        public string FieldDisplay { get; set; }
        public string FieldValue { get; set; }
        public bool required { get; set; }
        public int FieldOrder { get; set; }
        public virtual Kaizen00016 Kaizen00016 { get; set; }
    }
}
