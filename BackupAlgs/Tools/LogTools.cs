using System.IO;
using System.Collections.Generic;
using System;

namespace BackupAlgs.Tools
{
    internal class LogTools
    {
        private static string LogPath = @"..\log.txt"; 
        public static List<string> Logs = new List<string>();

        public static bool LogFileExists()
        {
            if (!File.Exists(LogPath))
                return false;
            else
                return true;
        }

        public static void LogFileCreate()
        {
            using (StreamWriter sw = new StreamWriter(LogPath)) { };
        }

        public static void AddNewLog(string log)
        {
            using StreamWriter sw = new StreamWriter(LogPath);
            {
                Logs.Add((Logs.Count + 1).ToString().PadRight(6) + " | " + DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss") + " | " + log);
                foreach(string item in Logs)
                {
                    sw.WriteLine(item);
                }
            }
        }

        public static void GetLogs()
        {
            using StreamReader sr = new StreamReader(LogPath);
            {
                while(!sr.EndOfStream)
                {
                    Logs.Add(sr.ReadLine());
                }
            }
        }
    }
}
