namespace Kaizen.Admin.Repository
{
    public class Sys00101Repository : GenericDataRepository<Sys00101>
    {
        public Sys00101Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
     
    }
}
