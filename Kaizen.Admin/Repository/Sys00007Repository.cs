namespace Kaizen.Admin.Repository
{
    public class Sys00007Repository : GenericDataRepository<Sys00007>
    {
        public Sys00007Repository(string CompanyID, string UserName, string UserPassword)
             : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
