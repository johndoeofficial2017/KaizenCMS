using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Kaizen.Configuration
{
    public partial class Kaizen004View
    {
        [NotMapped]
        public string MenuID
        {
            get
            {
                return ModuleID.ToString() + "_"+ MenueTypeID.ToString() + "_" + TaskID.ToString();
            }
        }

    }
}
