namespace Kaizen.Configuration.Repository
{
    public class CompanyAccessRepository : GenericDataRepository<CompanyAccess>
    {
        public CompanyAccessRepository()
           : base()
        {
        }
        public CompanyAccessRepository(string _UserName, string _UserPassword)
            : base(_UserName, _UserPassword)
        {
        }
    }
}
