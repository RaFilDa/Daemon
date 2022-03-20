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
                    Console.WriteLine($"{typeBackup} | BACKUP SUCCESSFUL");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK | BACKUP SUCCESSFUL");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                catch(Exception e)
                {
                    ErrorHandler.ThrowError(typeBackup, "BACKUP FAILED");
                }
            }
        }

        private static void StartBackup(string typeBackup, string pathSource, string pathDestination)
        {
            BackupTools.NewLists();
            
            typeBackup += "_BACKUP";

            string infoPath = pathDestination + @$"\{typeBackup}\";

            Directory.CreateDirectory(infoPath);

            if (!BackupTools.CheckForFile(infoPath))
                BackupTools.UpdateFile(infoPath, DateTime.MinValue.ToString(), BackupTools.RETENTION, typeBackup == "FULL_BACKUP" ? null : BackupTools.PACKAGES, "1");

            pathDestination = pathDestination + @$"\{typeBackup}\" + "BACKUP_" + BackupTools.GetInfo(infoPath)[3] + pathSource.Remove(0, pathSource.LastIndexOf("\\"));

            Directory.CreateDirectory(pathDestination);

            if (typeBackup != "FULL_BACKUP")
            {
                if (Convert.ToInt32(BackupTools.GetInfo(infoPath)[2]) < BackupTools.PACKAGES)
                    pathDestination += @"\PACKAGE_" + ((Convert.ToInt32(BackupTools.GetInfo(infoPath)[2]) - 6) * -1) + "\\";
                else
                    pathDestination += @"\FULL\";
                Directory.CreateDirectory(pathDestination);
            }

            DateTime snapshot = DateTime.Parse(BackupTools.GetInfo(infoPath)[0]);

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
            
            if(int.Parse(BackupTools.GetInfo(infoPath)[2]) == 5 || typeBackup != "DIFF_BACKUP")
                BackupTools.UpdateFile(infoPath, DateTime.Now.ToString(), Convert.ToInt32(BackupTools.GetInfo(infoPath)[1]), BackupTools.GetInfo(infoPath)[2] == "" ? null : Convert.ToInt32(BackupTools.GetInfo(infoPath)[2]) - 1, BackupTools.GetInfo(infoPath)[3]);
            else if(typeBackup == "DIFF_BACKUP")
                BackupTools.UpdateFile(infoPath, BackupTools.GetInfo(infoPath)[0], Convert.ToInt32(BackupTools.GetInfo(infoPath)[1]), BackupTools.GetInfo(infoPath)[2] == "" ? null : Convert.ToInt32(BackupTools.GetInfo(infoPath)[2]) - 1, BackupTools.GetInfo(infoPath)[3]);

            if (BackupTools.GetInfo(infoPath)[2] == "0")
            {
                BackupTools.Pack(infoPath);
            }
            //BackupTools.LogFiles(infoPath);
        }
    }
}