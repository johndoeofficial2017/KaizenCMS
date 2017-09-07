using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Kaizen.BusinessLogic.Configuration
{
    public class Kaizen00009Services
    {
        #region Infrastructure

        private Kaizen00009Repository _Kaizen00009Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public Kaizen00009Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            if (_UserContext == null)
            {
                _UserContext = new KaizenSession();
            }
            UserContext = _UserContext;
            _Kaizen00009Repository = new Kaizen00009Repository(UserContext.UserName, UserContext.Password);
        }
        #endregion

        public List<Kaizen00009> GetDynamicTableField(int ScreenID, string kaizenTableName)
        {
            //var tasks = _Kaizen00009Repository.GetAll(x => x.ScreenID == ScreenID);
            //return tasks.ToList();
            return null;
        }

        public List<Kaizen00009> GetAllDropDownFieldValues(string ExtraFieldID)
        {
            var tasks = _Kaizen00009Repository.GetAll(x => x.ExtraFieldID == ExtraFieldID);
            return tasks.ToList();
        }
        public bool Update(Kaizen00009 UpdateKaizen00009)
        {
            _Kaizen00009Repository.Update(UpdateKaizen00009);
            return true;
        }
        public bool AddKaizen00009(Kaizen00009 newTask)
        {
            try
            {
                _Kaizen00009Repository.Add(newTask);

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
        public bool Delete(IList<Kaizen00009> newTask)
        {
            if (newTask == null) return false;
            string str = string.Empty;
            foreach (var item in newTask)
            {
                str += item.DropValueID + ",";
            }
            str = str.Substring(0, str.Length - 1);
            _Kaizen00009Repository.ExecuteSqlCommand("delete Kaizen00009 where DropValueID in(" + str + ");");
            return true;
        }        
    }
}
