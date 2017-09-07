namespace Kaizen.SOP.Repository
{
    public class SOP00200Repository : GenericDataRepository<SOP00200> 
    {
        public SOP00200Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
