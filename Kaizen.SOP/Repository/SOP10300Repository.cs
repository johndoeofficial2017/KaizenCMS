namespace Kaizen.SOP.Repository
{
    public class SOP10300Repository : GenericDataRepository<SOP10300>
    {
        public SOP10300Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
