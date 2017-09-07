namespace Kaizen.Configuration.Repository
{
    public class KaizenSequenceRepository : GenericDataRepository<KaizenSequence>
    {
        public KaizenSequenceRepository(string _UserName, string _UserPassword)
            : base(_UserName, _UserPassword)
        {
        }
    }
}
