using System.Collections.Generic;

namespace Kaizen.CMS
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
