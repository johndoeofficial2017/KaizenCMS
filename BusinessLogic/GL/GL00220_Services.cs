using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.GL.Repository;
using Kaizen.GL;
using Kaizen.GL.DAL;
using System.Data.SqlClient;
using System.Data;

namespace Kaizen.BusinessLogic.GL
{
    public class GL00220Services
    {
        #region Infrastructure

        private Kaizen.GL.Repository.GL00220Repository _GL00220DataRepository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public GL00220Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _GL00220DataRepository = new GL00220Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public IList<GL00220> GetAll()
        {
            var tasks = _GL00220DataRepository.GetAll();
            IList<GL00220> result = tasks;
            return result;
        }
        public KaizenResultWithData GetAllByKaizenPublicKey(Guid KaizenPublicKey)
        {
            var tasks = _GL00220DataRepository.GetAll(xx => xx.KaizenPublicKey == KaizenPublicKey);
            KaizenResultWithData result = new KaizenResultWithData()
            {
                Status = true,
                Massage = "Successfully Completed",
                Data = tasks.ToList<object>()
            };
            return result;
        }

        public KaizenResult AddGL00220(GL00220 newTask)
        {
            KaizenResult result = _GL00220DataRepository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddGL00220(IList<GL00220> newTask)
        {
            KaizenResult result = _GL00220DataRepository.AddKaizenObject(newTask.ToArray());
            return result;
        }
        public KaizenResult Upload(IList<Kaizen.Configuration.Kaizen00001> KaizenColumn, string destinationPath)
        {
            Configuration.CompanyacessServices srv = new Configuration.CompanyacessServices(UserContext);

            using (var context = new GLContext(UserContext.CompanyID , UserContext.UserName , UserContext.Password))
            {
                context.Database.Connection.ConnectionString = context.Database.Connection.ConnectionString.Replace("TWO",this.UserContext.CompanyID.Trim());
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(context.Database.Connection.ConnectionString, SqlBulkCopyOptions.KeepNulls & SqlBulkCopyOptions.Default))
                {
                    foreach (Kaizen.Configuration.Kaizen00001 collumn in KaizenColumn)
                    {
                        bulkCopy.ColumnMappings.Add(collumn.FieldValue, collumn.FieldName);
                    }
                    bulkCopy.BatchSize = 10000;
                    bulkCopy.DestinationTableName = "GL00220";
                    KaizenResult result = new KaizenResult();
                    try
                    {
                        DataTable dt = srv.ReadExcelSheet(destinationPath);
                        dt.Columns.Add("KaizenPublicKey", typeof(Guid));
                        foreach (dynamic item in bulkCopy.ColumnMappings)
                        {
                            if (item.DestinationColumn == "ACTNUMBR")
                            {
                                string str = item.SourceColumn;
                                foreach (DataRow row in dt.Rows)
                                {
                                    row[str] = RemoveSpecialCharacters(row[str].ToString());
                                }
                            }
                            if (item.DestinationColumn == "KaizenPublicKey")
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    row["KaizenPublicKey"] = Guid.Parse(UserContext.KaizenPublicKey.ToString());
                                }
                            }
                        }
                        bulkCopy.WriteToServer(dt.CreateDataReader());
                        if(ValidGLUploading(UserContext.KaizenPublicKey.ToString()))
                        {
                            result.Status = true;
                            result.Massage = "Data has been Uploaded Successfully";
                            return result;
                        }
                        else
                        {
                            result.Status = false;
                            result.Massage = "Validation Failed For Uploaded Data";
                            return result;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Status = false;
                        result.Massage = "Failed to Upload Data";
                        return result;
                    }
                }
            }
        }
        public bool ValidGLUploading(string KaizenPublicKey)
        {
            ERPEntities ee = new ERPEntities();
            var result = ee.GL_ValidGLUploading(Guid.Parse(KaizenPublicKey));
            string re = result.ToString();
            if (re.Contains("OK"))
                return true;
            else
                return false;
        }

        public KaizenResult Update(GL00220 newTask)
        {
            KaizenResult result = _GL00220DataRepository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<GL00220> newTask)
        {
            KaizenResult result = _GL00220DataRepository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(GL00220 newTask)
        {
            KaizenResult result = _GL00220DataRepository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<GL00220> newTask)
        {
            KaizenResult result = _GL00220DataRepository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

    }
}
