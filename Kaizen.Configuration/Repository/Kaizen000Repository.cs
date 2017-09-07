using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Configuration.Repository
{
    public class Kaizen000Repository : GenericDataRepository<Kaizen000>
    {
        public Kaizen000Repository(string _UserName, string _UserPassword)
            : base(_UserName, _UserPassword)
        {
        }
    }
}
