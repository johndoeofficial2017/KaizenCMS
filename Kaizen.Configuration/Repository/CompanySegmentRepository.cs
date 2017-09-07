namespace Kaizen.Configuration.Repository
{
    public class CompanySegmentRepository : GenericDataRepository<CompanySegment>
    {
        public CompanySegmentRepository(string _UserName, string _UserPassword)
           : base(_UserName, _UserPassword)
        {
        }
    }
}
