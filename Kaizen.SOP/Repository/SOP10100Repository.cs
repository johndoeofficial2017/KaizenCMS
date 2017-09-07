namespace Kaizen.SOP.Repository
{
    public class SOP10100Repository : GenericDataRepository<SOP10100>
    {
        public SOP10100Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
