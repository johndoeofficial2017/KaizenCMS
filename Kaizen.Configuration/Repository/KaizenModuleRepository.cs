namespace Kaizen.Configuration.Repository
{
    public class KaizenModuleRepository : GenericDataRepository<Kaizen000>
    {
        public KaizenModuleRepository(string _UserName, string _UserPassword)
           : base(_UserName, _UserPassword)
        {
        }
    }
}
