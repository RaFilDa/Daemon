using System;
using BackupAlgs.Tools;
using System.IO;

namespace BackupAlgs.Backup
{
    internal class Full
    {
        private string PathSource = Paths.AllPaths[1];
        private string PathDestination = Paths.AllPaths[2];

        public Full(int startLine)
        {
            if(BackupTools.BackupCheck("FULL", startLine))
            {
                try
                {
                    StartBackup();
                    LogTools.AddNewLog("FULL BACKUP SUCCESSFUL");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK | BACKUP SUCCESSFUL");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                catch
                {
                    ErrorHandler.ThrowError("FULL", "BACKUP FAILED");
                }
            }
        }

        private void StartBackup()
        {
            BackupTools.NewLists();

            string backupFilePath = PathDestination + @"\FULL_BACKUP\";
            PathDestination = PathDestination + @"\FULL_BACKUP\" + PathSource.Remove(0, PathSource.LastIndexOf(@"\"));

            Directory.CreateDirectory(PathDestination);

            foreach (string dir in Directory.GetDirectories(PathSource, "*", SearchOption.AllDirectories))
            {
                BackupTools.Dirs.Add(dir);
                Directory.CreateDirectory(dir.Replace(PathSource, PathDestination));
            }

            foreach (string file in Directory.GetFiles(PathSource, "*.*", SearchOption.AllDirectories))
            {
                BackupTools.Files.Add(file);
                File.Copy(file, file.Replace(PathSource, PathDestination), true);
            }

            BackupTools.LogFiles(backupFilePath);
        }
    }
}
