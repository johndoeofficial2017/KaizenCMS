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
    public class MS_00001Repository : GenericDataRepository<MS_00001>
    {
        public MS_00001Repository(string CompanyID, string UserName, string UserPassword)
             : base(CompanyID, UserName, UserPassword)
        {
        }
     
    }
}
