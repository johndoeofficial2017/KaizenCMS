using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.OleDb;
namespace Kaizen.BusinessLogic
{
   public class Help
    {
        public static string GetFilter(string FieldID, KaizenFilterOperator FilterOperator, string Searchcritaria)
        {
            string result = "{0} {1} {2}";
            switch (FilterOperator)
            {
                case KaizenFilterOperator.StartsWith:
                    result = string.Format(result, FieldID, "LIKE", "'" + Searchcritaria + "%'");
                    break;
                case KaizenFilterOperator.EndsWith:
                    result = string.Format(result, FieldID, "LIKE", "'%" + Searchcritaria + "'");
                    break;
                case KaizenFilterOperator.Contains:
                    result = string.Format(result, FieldID, "LIKE", "'%" + Searchcritaria + "%'");
                    break;
                case KaizenFilterOperator.DoesNotContain:
                    result = string.Format(result, FieldID, "NOT LIKE", "'%" + Searchcritaria + "%'");
                    break;
                case KaizenFilterOperator.IsEqualTo:
                    result = string.Format(result, FieldID, "=\'", Searchcritaria + "\'");
                    break;
                case KaizenFilterOperator.IsNotEqualTo:
                    result = string.Format(result, FieldID, "<>", "'" + Searchcritaria + "'");
                    break;
                case KaizenFilterOperator.IsGreaterThan:
                    result = string.Format(result, FieldID, ">", "'" + Searchcritaria + "'");
                    break;
                case KaizenFilterOperator.IsGreaterThanOrEqualTo:
                    result = string.Format(result, FieldID, ">=", "'" + Searchcritaria + "'");
                    break;
                case KaizenFilterOperator.IsLessThan:
                    result = string.Format(result, FieldID, "<", "'" + Searchcritaria + "'");
                    break;
                case KaizenFilterOperator.IsLessThanOrEqualTo:
                    result = string.Format(result, FieldID, "<=", "'" + Searchcritaria + "'");
                    break;
            }
            return result;
        }

        public static DataTable ReadExcelSheet(string FilePath)
       {
           try
           {
               string conStr = "";
               switch (Path.GetExtension(FilePath))
               {
                   case ".xls":
                       conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                       break;
                   case ".xlsx":
                       conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                       break;
               }
               conStr = String.Format(conStr, FilePath, true);
               OleDbConnection connExcel = new OleDbConnection(conStr);
               OleDbCommand cmdExcel = new OleDbCommand();
               OleDbDataAdapter oda = new OleDbDataAdapter();
               DataTable dt = new DataTable();
               cmdExcel.Connection = connExcel;
               
               //Get the name of First Sheet
               connExcel.Open();
               DataTable dtExcelSchema;
               dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
               string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
               connExcel.Close();

               //Read Data from First Sheet
               connExcel.Open();
               cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
               oda.SelectCommand = cmdExcel;
               oda.Fill(dt);
               connExcel.Close();
               return dt;
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

  
    }
}
