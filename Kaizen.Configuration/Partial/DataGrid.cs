using System;
using System.Collections.Generic;
namespace Kaizen.Configuration
{
    public class column
    {
        public bool encoded { get; set; }
        public string title { get; set; }
        public string width { get; set; }
        public string template { get; set; }
        public string field { get; set; }
        public Filterable filterable { get; set; }
        public Boolean? hidden { get; set; }
    }
    public class DataGrid
    {
        public List<column> columns { get; set; }
        public string page { get; set; }
        public string pageSize { get; set; }
        public string sort { get; set; }
        public string filter { get; set; }
        public string group { get; set; }
        public string DataString { get; set; }
    }
    public class Filterable
    {
    }



}
