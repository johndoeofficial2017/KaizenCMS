namespace Kaizen.SOP.Repository
{
    public class SOP00201Repository : GenericDataRepository<SOP00201>
    {
        public SOP00201Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
