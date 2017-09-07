using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Admin
{
    public class DataCollection<T>
      where T : class
    {
        public List<T> Items { get; set; }
        public int TotalItemCount { get; set; }
        public int ThisPageItemCount { get; set; }
        public int TotalPageCount { get; set; }
    }
}
