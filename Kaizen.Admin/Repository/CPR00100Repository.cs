using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin;
using Kaizen.Admin.DAL;
namespace Kaizen.Admin.Repository
{
    public class CPR00100Repository : GenericDataRepository<CPR00100>
    {
        public CPR00100Repository(string CompanyID, string UserName, string UserPassword)
             : base(CompanyID, UserName, UserPassword)
        {
        }
     
    }
    public class CPR00101Repository : GenericDataRepository<CPR00101>
    {
        public CPR00101Repository(string CompanyID, string UserName, string UserPassword)
             : base(CompanyID, UserName, UserPassword)
        {
        }

    }
    public class CPR00001Repository : GenericDataRepository<CPR00001>
    {
        public CPR00001Repository(string CompanyID, string UserName, string UserPassword)
             : base(CompanyID, UserName, UserPassword)
        {
        }

    }
}
