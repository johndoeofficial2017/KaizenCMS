using System;
using System.IO;
using System.Web;


namespace Kaizen.Configuration
{
    public class ExceptionUtility
    {
        private ExceptionUtility()
        { }
        public static void LogException(Exception exc, string source)
        {
            string logFile = "~/ErrorLog.txt";
            logFile = HttpContext.Current.Server.MapPath(logFile);
            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            if (exc.InnerException != null)
            {
                sw.Write("Inner Exception Type: ");
                sw.WriteLine(exc.InnerException.GetType().ToString());
                sw.Write("Inner Exception: ");
                sw.WriteLine(exc.InnerException.Message);
                sw.Write("Inner Source: ");
                sw.WriteLine(exc.InnerException.Source);
                if (exc.InnerException.StackTrace != null)
                {
                    sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(exc.InnerException.StackTrace);
                }
            }
            sw.Write("Exception Type: ");
            sw.WriteLine(exc.GetType().ToString());
            sw.WriteLine("Exception: " + exc.Message);
            sw.WriteLine("Source: " + source);
            sw.WriteLine("Stack Trace: ");
            if (exc.StackTrace != null)
            {
                sw.WriteLine(exc.StackTrace);
                sw.WriteLine();
            }
            sw.Close();
        }
        public static void LogData(string logTxt)
        {
            string logFile = "~/Log.txt";
            logFile = HttpContext.Current.Server.MapPath(logFile);
            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            sw.WriteLine("Source: " + logTxt);
            sw.Close();
        }
        public static void NotifySystemOps(Exception exc)
        {
            // Include code for notifying IT system operators
        }
    }
}
