using System;
using BackupAlgs.Tools;
using System.IO;

namespace BackupAlgs.Backup
{
    internal class Incremental
    {
        private string PathSource = Paths.AllPaths[1];
        private string PathDestination = Paths.AllPaths[2];
        private int StartLine { get; set; }

        public Incremental(int startLine)
        {
            if (BackupTools.BackupCheck("INCREMENTAL", startLine))
            {
                try
                {
                    StartBackup();
                    LogTools.AddNewLog("INCREMENTAL BACKUP SUCCESSFUL");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK | BACKUP SUCCESSFUL");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                catch
                {
                    ErrorHandler.ThrowError("INCREMENTAL", "BACKUP FAILED");
                }
            }
        }

        public void StartBackup()
        {
            BackupTools.NewLists();

            PathDestination = PathDestination + @"\INCREMENTAL_BACKUP\";
            if(!Directory.Exists(PathDestination))
                Directory.CreateDirectory(PathDestination);
            if (!BackupTools.CheckForFile(PathDestination))
            {
                BackupTools.UpdateFile(PathDestination, 0);
                Backup();
            }
            else
            {
                Backup();
            }
        }

        public void Backup()
        {
            string backupFilePath = PathDestination;
            if (!File.Exists(PathDestination + @"backup_file_info.txt"))
            {
                PathDestination = PathDestination + @"\FULL\" + PathSource.Remove(0, PathSource.LastIndexOf(@"\"));

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
            else
            {
                BackupTools.LoadFiles(backupFilePath);
                
                PathDestination = PathDestination + @"\DIFF " + (BackupTools.GetInfo(backupFilePath) + 1) + @"\" + PathSource.Remove(0, PathSource.LastIndexOf(@"\"));

                foreach (string dir in Directory.GetDirectories(PathSource, "*", SearchOption.AllDirectories))
                {
                    if(!BackupTools.Dirs.Contains(dir))
                    {
                        BackupTools.Dirs.Add(dir);
                        BackupTools.NewDirs.Add(dir);
                    }
                }

                foreach (string file in Directory.GetFiles(PathSource, "*", SearchOption.AllDirectories))
                {
                    if (!BackupTools.Files.Contains(file))
                    {
                        BackupTools.Files.Add(file);
                        BackupTools.NewFiles.Add(file);
                    }
                }

                foreach(string dir in BackupTools.NewDirs)
                {
                    Directory.CreateDirectory(dir.Replace(PathSource, PathDestination));
                }

                foreach (string file in BackupTools.NewFiles)
                {
                    File.Copy(file, file.Replace(PathSource, PathDestination), true);
                }

                BackupTools.UpdateFile(backupFilePath, BackupTools.GetInfo(backupFilePath) + 1);
                BackupTools.LogFiles(backupFilePath);

                if (BackupTools.GetInfo(backupFilePath) == BackupTools.Retention)
                {
                    PathDestination = PathDestination + @"\FULL\" + PathSource.Remove(0, PathSource.LastIndexOf(@"\"));
                    for (int i = 1; i < BackupTools.Retention + 1; i++)
                    {
                        PathSource = backupFilePath + @"\DIFF " + i + @"\" + PathSource.Remove(0, PathSource.LastIndexOf(@"\"));
                        foreach (string dir in Directory.GetDirectories(PathSource, "*", SearchOption.AllDirectories))
                        {
                            Directory.CreateDirectory(dir.Replace(PathSource, PathDestination));
                        }

                        foreach (string file in Directory.GetFiles(PathSource, "*.*", SearchOption.AllDirectories))
                        {
                            File.Copy(file, file.Replace(PathSource, PathDestination), true);
                        }
                        DirectoryInfo d = new DirectoryInfo(PathSource);
                        d.Delete(true);
                        Directory.Delete(PathSource);
                    }
                    BackupTools.UpdateFile(backupFilePath, 0);
                }
            }
        }
    }
}
