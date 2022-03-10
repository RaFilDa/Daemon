using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackupAlgs.Tools;
using System.Threading;

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
            Thread.Sleep(500);
            if (PathTools.PathCheckSource())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK | Source was accepted");

                Thread.Sleep(500);

                if (PathTools.PathCheckDest())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK | Destination was accepted");

                    LogTools.AddNewLog("BACKUP: Successful FULL BACKUP");
                }
                else
                    ErrorHandler.ThrowError("FULL", "Destination path is missing/invalid");
            }
            else
                ErrorHandler.ThrowError("FULL", "Source path is missing/invalid");
        }
    }
}
