using System;
using System.Web.Mvc;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Reflection;

namespace UIServer
{
    public class KaizenJson
    {
        public bool Status
        {
            set;
            get;
        }
        public string Massage
        {
            set;
            get;
        }
        public object Data
        {
            set;
            get;
        }
    }
    public class KaizenFilter
    {
        public string FieldID
        {
            set;
            get;
        }
        public string FieldValue
        {
            set;
            get;
        }
    }
    public class FractionalNumber
    {
        public Double Result
        {
            get { return this.result; }
            private set { this.result = value; }
        }
        private Double result;

        public FractionalNumber(String input)
        {
            this.Result = this.Parse(input);
        }

        private Double Parse(String input)
        {
            input = (input ?? String.Empty).Trim();
            if (String.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input");
            }

            // standard decimal number (e.g. 1.125)
            if (input.IndexOf('.') != -1 || (input.IndexOf(' ') == -1 && input.IndexOf('/') == -1 && input.IndexOf('\\') == -1))
            {
                Double result;
                if (Double.TryParse(input, out result))
                {
                    return result;
                }
            }

            String[] parts = input.Split(new[] { ' ', '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);

            // stand-off fractional (e.g. 7/8)
            if (input.IndexOf(' ') == -1 && parts.Length == 2)
            {
                Double num, den;
                if (Double.TryParse(parts[0], out num) && Double.TryParse(parts[1], out den))
                {
                    return num / den;
                }
            }

            // Number and fraction (e.g. 2 1/2)
            if (parts.Length == 3)
            {
                Double whole, num, den;
                if (Double.TryParse(parts[0], out whole) && Double.TryParse(parts[1], out num) && Double.TryParse(parts[2], out den))
                {
                    return whole + (num / den);
                }
            }

            // Bogus / unable to parse
            return Double.NaN;
        }

        public override string ToString()
        {
            return this.Result.ToString();
        }

        public static implicit operator Double(FractionalNumber number)
        {
            return number.Result;
        }
    }
    public static class RazorViewToString
    {
        public static string RenderRazorViewToString(this Controller controller, string viewName, object model)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller", "Extension method called on a null controller");
            }

            if (controller.ControllerContext == null)
            {
                return string.Empty;
            }

            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                try
                {
                    var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                    return sw.GetStringBuilder().ToString();

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

    }
    public static class Extension
    {
        public static int WordCount(this string str)
        {
            string[] userString = str.Split(new char[] { ' ', '.', '?' },
                                        StringSplitOptions.RemoveEmptyEntries);
            int wordCount = userString.Length;
            return wordCount;
        }
        public static int TotalCharWithoutSpace(this string str)
        {
            int totalCharWithoutSpace = 0;
            string[] userString = str.Split(' ');
            foreach (string stringValue in userString)
            {
                totalCharWithoutSpace += stringValue.Length;
            }
            return totalCharWithoutSpace;
        }
        public static string RemoveSpecialCharacters(this string str)
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

        public static double TruncateToDecimalPlace(this double num, int? decimalPlaces)
        {
            if (decimalPlaces == null)
                return num;
            else
            {
                double power = (double)(Math.Pow(10.0, (double)decimalPlaces));
                return Math.Truncate((power * num)) / power;
            }
        }
        public static decimal TruncateToDecimalPlace(this decimal numberToTruncate, int? decimalPlaces)
        {
            if (decimalPlaces == null)
                return numberToTruncate;
            else
            {
                decimal power = (decimal)(Math.Pow(10.0, (double)decimalPlaces));
                return Math.Truncate((power * numberToTruncate)) / power;
            }
        }
        public static DataTable ToDataTable<T>(this IList<T> data) where T : class
        {
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }

    public class TempTable
    {
        public string TableName { get; set; }
        public List<Kaizen.Configuration.ExcelColumns> ExcelColumns { get; set; }
    }
}