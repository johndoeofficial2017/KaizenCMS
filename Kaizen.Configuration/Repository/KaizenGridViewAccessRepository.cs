namespace Kaizen.Configuration.Repository
{
    public class KaizenGridViewAccessRepository : GenericDataRepository<KaizenGridViewAccess>
    {
        public KaizenGridViewAccessRepository(string _UserName, string _UserPassword)
           : base(_UserName, _UserPassword)
        {
        }
    }
}
