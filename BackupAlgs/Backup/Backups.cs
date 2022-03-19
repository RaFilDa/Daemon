using System;
using System.IO;
using BackupAlgs.Tools;

namespace BackupAlgs.Backup
{
    public abstract class Backups
    {
        public static void Backup(int backup)
        {
            string typeBackup = "";
            switch (backup)
            {
                case 0:
                    typeBackup = "FULL";
                    break;
                case 1:
                    typeBackup = "DIFF";
                    break;
                case 2:
                    typeBackup = "INC";
                    break;
            }
            
            if(BackupTools.BackupCheck(typeBackup, 8))
            {
                try
                {
                    StartBackup(typeBackup, Paths.AllPaths[1], Paths.AllPaths[2]);
                    LogTools.AddNewLog($"{typeBackup} Backup Successful");
                    Console.WriteLine($"{typeBackup} | BACKUP SUCCESSFUL");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK | BACKUP SUCCESSFUL");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                catch
                {
                    ErrorHandler.ThrowError(typeBackup, "BACKUP FAILED");
                }
            }
        }

        private static void StartBackup(string typeBackup, string pathSource, string pathDestination)
        {
            BackupTools.NewLists();

            //TODO change snapshot to latest files
            
            typeBackup += "_BACKUP";
            DateTime snapshot = DateTime.MinValue;

            switch (typeBackup)
            {
                case "DIFF_BACKUP":
                    snapshot = DateTime.Now - TimeSpan.FromMinutes(10);
                    break;
                case "INC_BACKUP":
                    snapshot = DateTime.Now - TimeSpan.FromHours(100000);
                    break;
            }
            
            pathDestination = pathDestination + @$"\{typeBackup}\" + pathSource.Remove(0, pathSource.LastIndexOf("\\"));

            Directory.CreateDirectory(pathDestination);

            foreach (string dir in Directory.GetDirectories(pathSource, "*", SearchOption.AllDirectories))
            {
                if(new DirectoryInfo(dir).LastWriteTime <= snapshot)
                    continue;
                BackupTools.Dirs.Add(dir);
                Directory.CreateDirectory(dir.Replace(pathSource, pathDestination));
            }

            foreach (string file in Directory.GetFiles(pathSource, "*.*", SearchOption.AllDirectories))
            {
                if(new FileInfo(file).LastWriteTime <= snapshot)
                    continue;
                BackupTools.Files.Add(file);
                File.Copy(file, file.Replace(pathSource, pathDestination), true);
            }

            BackupTools.LogFiles(pathDestination.Substring(0, pathDestination.LastIndexOf('\\')));
        }
    }
}