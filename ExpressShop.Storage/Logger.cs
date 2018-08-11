using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressShop.Storage
{
    public static class Logger
    {
        static object locker = new object();

        static Logger()
        {
            var rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            LogFilePath = $"{rootPath}/ReserveTestResult.log";
        }

        private static string LogFilePath { get; }

        /// <summary>
        /// Записать логи в файл
        /// </summary>
        public static void Write(string text)
        {
            lock (locker)
            {
                using (var sw = new StreamWriter(LogFilePath, true, Encoding.UTF8))
                {
                    sw.WriteLine(text);
                }
            }
        }

        /// <summary>
        /// Очистить файл логов
        /// </summary>
        public static void ClearLogs()
        {
            File.Create(LogFilePath).Close();
        }
    }
}
