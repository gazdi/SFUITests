using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFRegression.Logger
{
    public class ConsoleLogger
    {
        private string _timestampformat;
        private int _minLevel;

        /// <summary>
        /// Set timestamp format and minimum log level to output
        /// </summary>
        /// <param name="timestampformat">timestamp format string</param>
        /// <param name="minLevel">minimum level to output
        /// CRITICAL: 4
        /// ERROR: 3
        /// DEBUG: 2
        /// INFO: 1
        /// </param>
        public void Configure(string timestampformat, int minLevel)
        {
            try
            {
                var tmp = new DateTime().ToString(timestampformat);
                _timestampformat = timestampformat;
            }
            catch (FormatException e)
            {
                Console.WriteLine("*** Error configuring the ConsoleLogger: {0}", e.Message);
            }

            if (minLevel < 1 || minLevel > 4)
            {
                Console.WriteLine("*** Error configuring the ConsoleLogger: invalid minLevel");
            }
            else
            {
                _minLevel = minLevel;
            }
        }

        public ConsoleLogger()
        {
            Configure("dd/MM/yyyy hh:mm:ss.zzz", 1);
        }

        protected void WriteLog(string level, string msg)
        {
            Console.WriteLine("{0}|{1}|{2}", new DateTime().ToString(_timestampformat), level, msg);
        }

        public void Critical(string msg) => WriteLog("CRITICAL", msg);
        public void Error(string msg) => WriteLog("ERROR", msg);
        public void Debug(string msg) => WriteLog("DEBUG", msg);
        public void Info(string msg) => WriteLog("INFO", msg);
    }
}
