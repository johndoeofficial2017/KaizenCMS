namespace Kaizen.Configuration.Repository
{
    public class CM00000Repository : GenericDataRepository<CM00000>
    {
        private string password;

        public CM00000Repository(string _UserName, string _UserPassword)
            : base(_UserName, _UserPassword)
        {
        }
        public CM00000Repository()
           : base()
        {
        }

        public CM00000Repository(string _UserName, string _UserPassword, string password) : base(_UserName, _UserPassword)
        {
            this.password = password;
        }
    }
}
