using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.SOP
{
    public class KaizenResult
    {
        public bool Status
        {
            set;
            get;
        }
        public string Massage
        {
            set;
            get;
        }
        public string Description
        {
            set;
            get;
        }
    }
    public class KaizenResultWithData
    {
        public bool Status
        {
            set;
            get;
        }
        public string Massage
        {
            set;
            get;
        }
        public List<object> Data
        {
            set;
            get;
        }
    }

}
