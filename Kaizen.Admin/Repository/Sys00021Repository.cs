﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Admin;
using Kaizen.Admin.DAL;
namespace Kaizen.Admin.Repository
{
    public class Sys00021Repository : GenericDataRepository<Sys00021>
    {
        public Sys00021Repository(string CompanyID, string UserName, string UserPassword)
            : base(CompanyID, UserName, UserPassword)
        {
        }
     
    }
}
