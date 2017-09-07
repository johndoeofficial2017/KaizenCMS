namespace Kaizen.SOP.Repository
{
    public class SOP00010Repository : GenericDataRepository<SOP00010>
    {
        public SOP00010Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
