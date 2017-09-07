namespace Kaizen.SOP.Repository
{
    public class Sys00005Repository : GenericDataRepository<Sys00005>
    {
        public Sys00005Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
    }
}
