namespace Kaizen.CMS.Repository
{
    public class CM_ClientRepository : GenericDataRepository<CM_Client>
    {
        public CM_ClientRepository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
