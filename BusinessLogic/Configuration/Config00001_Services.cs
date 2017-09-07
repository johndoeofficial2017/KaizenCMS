using System;

using Kaizen.Configuration.Repository;
using Kaizen.Configuration;

namespace Kaizen.BusinessLogic.Configuration
{
    public class Config00001Services
    {
       #region Infrastructure

        private Config00001Repository _Config00001Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Config00001Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _Config00001Repository = new Config00001Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion

   
   

        public bool AddConfig00001(Config00001 newTask)
        {
            try
            {
                _Config00001Repository.Add(newTask);
                Config00001 con = Master.GetConfigHR(UserContext.CompanyID.Trim());
                if (!con.IsAutoItemID)
                {
                    return true;
                }
                else if (con.IsAutoItemIDByCat)
                {
                    return true;
                }

                string SequenceName = "CREATE SEQUENCE PrefixCategory_" + UserContext.CompanyID + "_Sequence START WITH 1 INCREMENT BY 1;";
                Kaizen.BusinessLogic.Configuration.KaizenSessionServices serviceConfiguration = new Configuration.KaizenSessionServices(UserContext);
                serviceConfiguration.ExecuteSqlCommand(SequenceName);
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(Config00001 UpdateConfig00001)
        {
            try
            {
                _Config00001Repository.Update(UpdateConfig00001);
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(string Config00001Name)
        {
            try
            {
                _Config00001Repository.ExecuteSqlCommand("delete Config00001s where Config00001Name='" + Config00001Name.Trim() + "'");
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _Config00001Repository.ExecuteSqlCommandInt(myQuery);
            return result;
        }
    }
}
