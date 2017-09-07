using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class Workstation
    {
        public int WorkstationID { get; set; }
        public string WorkstationName { get; set; }
        public string WorkstationAddress { get; set; }
        public string WorkstationCpuID { get; set; }
        public Nullable<bool> WorkstationIsActive { get; set; }
        public System.DateTime RegisterDate { get; set; }
    }
}
