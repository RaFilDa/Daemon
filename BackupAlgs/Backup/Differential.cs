using System;
using BackupAlgs.Tools;
using System.IO;

namespace BackupAlgs.Backup
{
    internal class Differential
    {
        private string PathSource = Paths.AllPaths[1];
        private string PathDestination = Paths.AllPaths[2];

        public Differential(int startLine)
        {
            if (BackupTools.BackupCheck("DIFFERENTIAL", startLine))
            {
                try
                {
                    StartBackup();
                    LogTools.AddNewLog("DIFFERENTIAL BACKUP SUCCESSFUL");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK | BACKUP SUCCESSFUL");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                catch
                {
                    ErrorHandler.ThrowError("DIFFERENTIAL", "BACKUP FAILED");
                }
            }
        }

        private void StartBackup()
        {

        }
    }
}
