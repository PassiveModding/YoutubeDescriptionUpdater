using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Youtube.NET
{
    public static class Log
    {
        public static string GetLogFilePath()
        {
            var baseDir = AppContext.BaseDirectory;
            var file = Path.Combine(baseDir, "Log.txt");
            return file;
        }

        public static void EnsureFileExists()
        {
            var path = GetLogFilePath();
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        static object locker = new object();

        public static Task LogMessage(string message)
        {
            lock (locker)
            {
                EnsureFileExists();
                message = $"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] {message}\r\n";
                File.AppendAllText(GetLogFilePath(), message);
                Console.WriteLine(message);
                return  Task.CompletedTask;
            }
        }
    }
}
