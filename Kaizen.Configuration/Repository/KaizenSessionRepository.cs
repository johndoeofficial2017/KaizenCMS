namespace Kaizen.Configuration.Repository
{
    public class KaizenSessionRepository : GenericDataRepository<KaizenSession>
    {
        public KaizenSessionRepository()
           : base()
        {
        }
        public KaizenSessionRepository(string _UserName, string _UserPassword)
            : base(_UserName, _UserPassword)
        {
        }
    }
}
