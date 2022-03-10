using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupAlgs.Tools
{
    public static class Paths
    {
        public static string PathLog = @"..\log.txt";
        public static string PathSource = "";
        public static string PathDest = @"C:\Users\Public\Documents\Backups";

        public static string[] AllPaths = { PathLog, PathSource, PathDest };
    }
}
