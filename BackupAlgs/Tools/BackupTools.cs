using System;
using System.Collections.Generic;
using System.IO;

namespace BackupAlgs.Tools
{
    public static class BackupTools
    {
        public static int Retention = 5;
        public static List<string> Dirs = new List<string>();
        public static List<string> Files = new List<string>();
        public static List<string> NewFiles = new List<string>();
        public static List<string> NewDirs = new List<string>();

        public static bool BackupCheck(string backupName, int startLine)
        {
            Console.SetCursorPosition(0, startLine);
            if (PathTools.PathCheckSource())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK | Source was accepted");
                if (PathTools.PathCheckDest())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK | Destination was accepted");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Backuping...");

                    return true;
                }
                else
                {
                    ErrorHandler.ThrowError("FULL", "Destination path is missing/invalid");
                    return false;
                }
            }
            else
            {
                ErrorHandler.ThrowError("FULL", "Source path is missing/invalid");
                return false;
            }
        }

        public static void NewLists()
        {
            Dirs = new List<string>();
            Files = new List<string>();
            NewDirs = new List<string>();
            NewFiles = new List<string>();
        }

        public static void LoadFiles(string path)
        {
            using StreamReader sw = new StreamReader(path + @"backup_file_info.txt");
            {
                sw.ReadLine();
                string dir = "";
                while(dir != "Files:")
                {
                    dir = sw.ReadLine();
                    if(dir != "Files:")
                        Dirs.Add(dir);
                }
                while(!sw.EndOfStream)
                {
                    Files.Add(sw.ReadLine());
                }
            }
        }

        public static void LogFiles(string path)
        {
            using StreamWriter sw = new StreamWriter(path + @"backup_file_info.txt");
            {
                sw.WriteLine("Dirs:");
                foreach (string item in Dirs)
                {
                    sw.WriteLine(item);
                }
                sw.WriteLine("Files:");
                foreach (string item in Files)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
            }
        }

        public static bool CheckForFile(string path)
        {
            if (!File.Exists(path + @"backup_retention_info.txt"))
                return false;
            else
                return true;
        }

        public static void UpdateFile(string path, int number)
        {
            using StreamWriter sw = new StreamWriter(path + @"backup_retention_info.txt");
            {
                sw.WriteLine(number);
                sw.Close();
            }
        }

        public static int GetInfo(string path)
        {
            string result = ""; 
            using StreamReader sr = new StreamReader(path + @"backup_retention_info.txt");
            {
                result = sr.ReadLine();
                sr.Close();
            }
            return Convert.ToInt32(result);
        }
    }
}
