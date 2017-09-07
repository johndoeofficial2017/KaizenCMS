namespace Kaizen.CMS.Repository
{
    public class CM_CaseViewRepository : GenericDataRepository<CM_CaseView>
    {
        public CM_CaseViewRepository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
