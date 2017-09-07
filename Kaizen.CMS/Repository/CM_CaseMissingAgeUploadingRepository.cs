using System.Collections.Generic;
using System.Linq;
using Kaizen.CMS.DAL;
namespace Kaizen.CMS.Repository
{
    public class CM_CaseMissingAgeUploadingRepository : GenericDataRepository<CM_CaseMissingAgeUploading>
    {
        public CM_CaseMissingAgeUploadingRepository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
