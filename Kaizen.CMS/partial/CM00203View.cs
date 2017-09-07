//using Kendo.Mvc.UI;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CMS_203View 
    {

        [NotMapped]
        public string Title { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public bool IsAllDay { get; set; }
        [NotMapped]
        public DateTime Start { get; set; }
        [NotMapped]
        public DateTime End { get; set; }
        [NotMapped]
        public string StartTimezone { get; set; }
        [NotMapped]
        public string EndTimezone { get; set; }
        [NotMapped]
        public string RecurrenceRule { get; set; }
        [NotMapped]
        public string RecurrenceException { get; set; }
    }
}
