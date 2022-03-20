using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackupAlgs.Tools
{
    public static class BackupTools
    {
        public const int RETENTION = 5;
        public const int PACKAGES = 5;
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
                    ErrorHandler.ThrowError(backupName, "Destination path is missing/invalid");
                    return false;
                }
            }
            else
            {
                ErrorHandler.ThrowError(backupName, "Source path is missing/invalid");
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
            using StreamReader sr = new StreamReader(path + @"backup_file_info.txt");
            {
                sr.ReadLine();
                string dir = "";
                while (dir != "Files:")
                {
                    dir = sr.ReadLine();
                    if (dir != "Files:")
                        Dirs.Add(dir);
                }
                while (!sr.EndOfStream)
                {
                    Files.Add(sr.ReadLine());
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
            if (!File.Exists(path + @"info.txt"))
                return false;
            else
                return true;
        }

        public static void UpdateFile(string path, string snapshot, int retention, int? packages, string number)
        {
            //1st line = last snapshot time
            //2nd line = RETENTION
            //3rd line = PACKAGES
            //4th line = number of backup

            using StreamWriter sw = new StreamWriter(path + @"info.txt");
            {
                sw.WriteLine(snapshot);
                sw.WriteLine(retention);
                sw.WriteLine(packages);
                sw.WriteLine(number);
                sw.Close();
            }
        }

        public static string[] GetInfo(string path)
        {
            string[] result = new string[4];
            int indexer = 0;
            using StreamReader sr = new StreamReader(path + @"info.txt");
            {
                while (!sr.EndOfStream)
                {
                    result[indexer] = sr.ReadLine();
                    indexer++;
                }
                sr.Close();
            }
            return result;
        }

        public static void Pack(string path)
        {
            int retention = Convert.ToInt32(GetInfo(path)[1]);
            if (retention == 1)
            {
                DeleteOldest(path);
                retention++;
            }
            UpdateFile(path, DateTime.MinValue.ToString(), retention - 1, PACKAGES, (Convert.ToInt32(GetInfo(path)[3]) + 1).ToString());
        } 

        public static void DeleteOldest(string path)
        {
            List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            foreach(string dir in Directory.GetDirectories(path))
            {
                dirs.Add(new DirectoryInfo(dir));
            }
     
            dirs.Sort((x, y) => x.CreationTime.CompareTo(y.CreationTime));
            dirs[0].Delete(true);
        }
    }
}
