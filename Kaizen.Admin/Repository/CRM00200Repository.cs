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
    public class CRM00200Repository : GenericDataRepository<CRM00200>
    {
        public CRM00200Repository(string CompanyID, string UserName, string UserPassword)
             : base(CompanyID, UserName, UserPassword)
        {
        }
     
    }
}
