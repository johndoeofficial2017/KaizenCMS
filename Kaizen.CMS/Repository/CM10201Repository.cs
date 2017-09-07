using System.Collections.Generic;
using System.Linq;
using Kaizen.CMS.DAL;
namespace Kaizen.CMS.Repository
{
    public class CM10201Repository : GenericDataRepository<CM10201>
    {
        public CM10201Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
