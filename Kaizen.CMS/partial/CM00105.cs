using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.CMS
{
    public partial class CM00105
    {
        [NotMapped]
        public string FullName
        {
            get
            {
                string fullname = this.FirstName + " " + this.MidName + " " + this.LastName;
                return fullname;
            }
        }
        [NotMapped]
        public List<CM00105> CM00105Children
        {
            get;
            set;
        }
    }
}
