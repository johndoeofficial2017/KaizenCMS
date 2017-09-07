using System.Collections.Generic;
using System.Linq;
using Kaizen.CMS.DAL;
namespace Kaizen.CMS.Repository
{
    public class CM00078Repository : GenericDataRepository<CM00078>
    {
        public CM00078Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
