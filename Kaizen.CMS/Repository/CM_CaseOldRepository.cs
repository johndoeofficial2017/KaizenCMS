namespace Kaizen.CMS.Repository
{
    public class CM_CaseOldRepository : GenericDataRepository<CM_CaseOld>
    {
        public CM_CaseOldRepository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
