namespace Kaizen.SOP.Repository
{
    public class SOP00003Repository : GenericDataRepository<SOP00003>
    {
        public SOP00003Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
