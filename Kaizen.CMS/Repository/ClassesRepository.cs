using System.Collections.Generic;
using System.Linq;
using Kaizen.CMS.DAL;
namespace Kaizen.CMS.Repository
{
    public class CM00010Repository : GenericDataRepository<CM00010>
    {
        public CM00010Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }

    }
}
