using System.Web;
using System.Web.Optimization;

namespace UIServer
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            BundleTable.EnableOptimizations = true;
        }
    }
}
