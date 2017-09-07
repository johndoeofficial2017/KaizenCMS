namespace Kaizen.SOP.Repository
{
    public class SOP00100Repository : GenericDataRepository<SOP00100>
    {
        public SOP00100Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
