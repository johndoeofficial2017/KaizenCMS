using System.Collections.Generic;
using System.Linq;
using Kaizen.CMS.DAL;
namespace Kaizen.CMS.Repository
{
    public class CM00207Repository : GenericDataRepository<CM00207>
    {
        public CM00207Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
