using Kaizen.CMS;
using Kaizen.CMS.Repository;
using System.Collections.Generic;
using System.Linq;
namespace Kaizen.HumanResources.Repository
{
    public class CM00105Repository : GenericDataRepository<CM00105>
    {
        public CM00105Repository(string CompanyID, string UserName, string UserPassword) : base(CompanyID, UserName, UserPassword)
        {

        }
        public List<KCM00203_Result> GetAgentListBySupervisor(string AgentID,System.DateTime StartDate, System.DateTime EndDate)
        {
            using (EDMXERPContext dataContext = new EDMXERPContext())
            {
                CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
                string UserPassword = cr.Decrypting(System.Configuration.ConfigurationManager.AppSettings["XXXX"]);

                string SSSS = System.Configuration.ConfigurationManager.AppSettings["SSSS"].ToString();
                string UUUU = System.Configuration.ConfigurationManager.AppSettings["UUUU"].ToString();
                dataContext.Database.Connection.ConnectionString = dataContext.Database.Connection.ConnectionString.Replace("TWO", CompanyID);
                dataContext.Database.Connection.ConnectionString = dataContext.Database.Connection.ConnectionString.Replace("XXXX", UserPassword);
                dataContext.Database.Connection.ConnectionString = dataContext.Database.Connection.ConnectionString.Replace("UUUU", UUUU);
                dataContext.Database.Connection.ConnectionString = dataContext.Database.Connection.ConnectionString.Replace("SSSS", SSSS.Trim());
                var o = (from row in dataContext.KCM00203(AgentID, StartDate, EndDate)
                         select row);
                return o.ToList();
            }
        }
    }
}
