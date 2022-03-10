using System;
using BackupAlgs.Tools;

namespace BackupAlgs.Backup
{
    internal class Full
    {
        private string PathSource = Paths.AllPaths[1];
        private string PathDestination = Paths.AllPaths[2];
        private int StartLine { get; set; }

        public Full(int startLine)
        {
            StartLine = startLine;
            Console.SetCursorPosition(0, StartLine);
            if (PathTools.PathCheckSource())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK | Source was accepted");
                if (PathTools.PathCheckDest())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK | Destination was accepted");
                    Console.WriteLine();

                    StartBackup();
                }
                else
                    ErrorHandler.ThrowError("FULL", "Destination path is missing/invalid");
            }
            else
                ErrorHandler.ThrowError("FULL", "Source path is missing/invalid");
        }

        public void StartBackup()
        {

        }
    }
}
