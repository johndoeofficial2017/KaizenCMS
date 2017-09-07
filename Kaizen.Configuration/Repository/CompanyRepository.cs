namespace Kaizen.Configuration.Repository
{
    public class CompanyRepository : GenericDataRepository<Company>
    {
        public CompanyRepository(string _UserName, string _UserPassword)
            : base(_UserName, _UserPassword)
        {
        }
        public CompanyRepository()
          : base()
        {
        }
    }
}
