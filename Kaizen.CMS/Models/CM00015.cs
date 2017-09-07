using System;
using System.Collections.Generic;

namespace Kaizen.CMS
{
    public partial class CM00015
    {
        public CM00015()
        {
            this.CM00203 = new List<CM00203>();
        }

        public int BucketID { get; set; }
        public string BucketName { get; set; }
        public virtual ICollection<CM00203> CM00203 { get; set; }
    }
}
