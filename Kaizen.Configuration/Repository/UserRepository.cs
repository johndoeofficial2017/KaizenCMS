namespace Kaizen.Configuration.Repository
{
    public class UserRepository : GenericDataRepository<User>
    {
        public UserRepository()
           : base()
        {
        }
        public UserRepository(string UserName,string UserPassword)
           : base(UserName, UserPassword)
        {
        }
    }
}
