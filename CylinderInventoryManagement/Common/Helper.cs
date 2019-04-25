using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CylinderInventoryManagement.Common
{
    public class Helper
    {
        public static void WriteErrorLog(string Message)
        {
            try
            {
                string sLogsDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                if (!Directory.Exists(sLogsDirectory))
                    Directory.CreateDirectory(sLogsDirectory);

                using (StreamWriter sw = new StreamWriter(sLogsDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true))
                {
                    sw.WriteLine("-------------------------------------------------------");
                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine("Date : " + DateTime.Now.ToString("HH:mm:ss.fff"));
                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine("Message : " + Message);
                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }
    }
}