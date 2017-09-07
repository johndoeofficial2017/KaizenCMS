namespace Kaizen.Admin.Repository
{
    public class Sys00100Repository : GenericDataRepository<Sys00100>
    {
        public Sys00100Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
     
    }
}
