using System.Collections.Generic;

namespace Kaizen.SOP
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
