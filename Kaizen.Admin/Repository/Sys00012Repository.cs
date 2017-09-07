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
    public class Sys00012Repository : GenericDataRepository<Sys00012>
    {
        public Sys00012Repository(string CompanyID, string UserName, string UserPassword)
           : base(CompanyID, UserName, UserPassword)
        {
        }
     
    }
}
