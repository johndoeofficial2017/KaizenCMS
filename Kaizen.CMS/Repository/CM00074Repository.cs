using System.Collections.Generic;
using System.Linq;
using Kaizen.CMS.DAL;
namespace Kaizen.CMS.Repository
{
    public class CM00074Repository : GenericDataRepository<CM00074>
    {
        public CM00074Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
