using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace Log
{
    public class Logger
    {
        private BlockingCollection<string> logs;
        private string _filePath;
        public Logger(string filePath)
        {
            logs = new BlockingCollection<string>();
            _filePath = filePath;
            ProcessLogsCollection();
       }

        public void LogError(string message)
        {
            LogData(LogType.ERROR, message);
        }
        public void LogInfo(string message)
        {
            LogData(LogType.INFO, message);
        }
        public void LogDebug(string message)
        {
            LogData(LogType.DEBUG, message);
        }

        private void LogData(LogType type, string message) {
            var log = string.Format("[{0} {1}]: {2}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), type, message);
            AddToBlockingCollection(log);
        }

        private void AddToBlockingCollection(string message)
        {
            if (!logs.IsAddingCompleted) {
                logs.Add(message);
            }
        }

        private void ProcessLogsCollection() {
            Task.Run(() =>
            {
                foreach (string item in logs.GetConsumingEnumerable())
                {
                    //log line to file
                    using (StreamWriter sw = File.AppendText(_filePath))
                    {
                        sw.WriteLine(item);
                    }
                }
            });
        }
    }
}
