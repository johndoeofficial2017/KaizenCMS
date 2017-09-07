namespace Kaizen.Configuration.Repository
{
    public class CHattingMessageRepository : GenericDataRepository<Kaizen00400>
    {
       
        public CHattingMessageRepository(string _UserName, string _UserPassword)
            : base(_UserName, _UserPassword)
        {

        }
    }
}
