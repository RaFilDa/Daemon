using System;
using System.IO;
using BackupAlgs.Tools;

namespace BackupAlgs.Backup
{
    public abstract class Backups
    {
        public static void Backup(int backup)
        {
            BackupTools bt = new BackupTools();

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
            
            if(bt.BackupCheck(typeBackup, 8))
            {
                try
                {
                    StartBackup(typeBackup, Paths.AllPaths[1], Paths.AllPaths[2]);
                    LogTools.AddNewLog($"{typeBackup} | BACKUP SUCCESFUL");
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
            BackupTools bt = new BackupTools();

            bt.NewLists();

            typeBackup += "_BACKUP";

            string infoPath = pathDestination + @$"\{typeBackup}\";

            Directory.CreateDirectory(infoPath);
            
            if (!bt.CheckForFile(infoPath))
                bt.UpdateFile(infoPath, DateTime.MinValue.ToString(), bt.RETENTION, typeBackup == "FULL_BACKUP" ? 1 : bt.PACKAGES, "1");
           
            pathDestination = pathDestination + @$"\{typeBackup}\" + "BACKUP_" + bt.GetInfo(infoPath)[3] + pathSource.Remove(0, pathSource.LastIndexOf("\\"));

            Directory.CreateDirectory(pathDestination);

            if (typeBackup != "FULL_BACKUP")
            {
                if (Convert.ToInt32(bt.GetInfo(infoPath)[2]) < bt.PACKAGES)
                    pathDestination += @"\PACKAGE_" + ((Convert.ToInt32(bt.GetInfo(infoPath)[2]) - 6) * -1) + "\\";
                else
                    pathDestination += @"\FULL\";
                Directory.CreateDirectory(pathDestination);
            }

            DateTime snapshot = DateTime.Parse(bt.GetInfo(infoPath)[0]);

            foreach (string dir in Directory.GetDirectories(pathSource, "*", SearchOption.AllDirectories))
            {
                if(new DirectoryInfo(dir).LastWriteTime <= snapshot)
                    continue;
                bt.Dirs.Add(dir);
                Directory.CreateDirectory(dir.Replace(pathSource, pathDestination));
            }

            foreach (string file in Directory.GetFiles(pathSource, "*.*", SearchOption.AllDirectories))
            {
                if(new FileInfo(file).LastWriteTime <= snapshot)
                    continue;
                bt.Files.Add(file);
                File.Copy(file, file.Replace(pathSource, pathDestination), true);
            }
            
            if(int.Parse(bt.GetInfo(infoPath)[2]) == 5 || typeBackup != "DIFF_BACKUP")
                bt.UpdateFile(infoPath, DateTime.Now.ToString(), Convert.ToInt32(bt.GetInfo(infoPath)[1]), Convert.ToInt32(bt.GetInfo(infoPath)[2]) - 1, bt.GetInfo(infoPath)[3]);
            else if(typeBackup == "DIFF_BACKUP")
                bt.UpdateFile(infoPath, bt.GetInfo(infoPath)[0], Convert.ToInt32(bt.GetInfo(infoPath)[1]), Convert.ToInt32(bt.GetInfo(infoPath)[2]) - 1, bt.GetInfo(infoPath)[3]);

            if (bt.GetInfo(infoPath)[2] == "0")
            {
                bt.Pack(infoPath, typeBackup);
            }

            bt.LogFiles(pathDestination);
        }
    }
}