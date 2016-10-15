using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Log;

namespace LogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Debug.Log");
            Logger log = new Logger(filePath);
            log.LogInfo("My first log");
            log.LogDebug("My second log");
            log.LogError("My third log");
        }
    }
}
