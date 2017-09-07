using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Kaizen00026
    {
        public int ViewID { get; set; }
        public string field { get; set; }
        public Nullable<short> FieldTypeID { get; set; }
        public string title { get; set; }
        public int width { get; set; }
        public int ColumnOrder { get; set; }
        public bool IsActive { get; set; }
        public bool locked { get; set; }
        public Nullable<bool> lockable { get; set; }
        public Nullable<bool> filterable { get; set; }
        public bool sortable { get; set; }
        public Nullable<bool> hidden { get; set; }
        public Nullable<bool> menu { get; set; }
        public string format { get; set; }
        public string template { get; set; }
        public string FieldEquation { get; set; }
        public bool IsPrintable { get; set; }
        public Nullable<bool> IsEmailable { get; set; }
        public virtual Kaizen00011 Kaizen00011 { get; set; }
    }
}
