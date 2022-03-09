using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BackupAlgs.Tools
{
    public static class PathTools
    {
        private static string path = @"..\paths.txt";

        public static string PathCheckSource()
        {
            string result = "";
            using StreamReader sr = new StreamReader(path);
            {
                for (int i = 0; i < 2; i++)
                {
                    if (i == 1)
                    {
                        result = sr.ReadLine();
                    }
                    else
                        sr.ReadLine();
                }
            }

            if (result == "")
                result = "ERROR";
            return result;
        }

        public static void PathUpdateFile()
        {
            using StreamWriter sw = new StreamWriter(path);
            {
                for (int i = 0; i < Paths.allPaths.Length; i++)
                {
                    sw.WriteLine(Paths.allPaths[i]);
                }
            }
        }
    }
}
