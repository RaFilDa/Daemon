using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupAlgs.Tools
{
    public static class ErrorHandler
    {
        public static void ThrowError(string backup, string errorMessage)
        {
            LogTools.AddNewLog($"ERROR: {backup} BACKUP - {errorMessage}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("X  | ERROR: " + errorMessage);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
