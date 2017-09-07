using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.CMS
{
    public class KaizenDataTable
    {
        public System.Data.DataTable Items { get; set; }
        public int TotalItemCount { get; set; }
        public int ThisPageItemCount { get; set; }
        public int TotalPageCount { get; set; }
    }
}
