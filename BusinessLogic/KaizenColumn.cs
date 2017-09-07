using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.BusinessLogic
{
    public class KaizenColumn
    {
        public string FieldName { get; set; }
        public string FieldDisplay { get; set; }
        public string FieldValue { get; set; }
        public bool required { get; set; }
        public int FieldOrder { get; set; }
        //public string SourceValue { get; set; }
        public string FixedValue { get; set; }
        public int FieldTypeID { get; set; }
    }
}
