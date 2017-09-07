namespace Kaizen.CMS.Repository
{
    public class CM00100ViewRepository : GenericDataRepository<CM00100View>
    {
        public CM00100ViewRepository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
