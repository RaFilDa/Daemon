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
        private static string SavePaths = @"..\paths.txt";

        public static bool PathFileExists()
        {
            if (!File.Exists(SavePaths))
                return false;
            else
                return true;
        }

        public static void PathFileCreate()
        {
            using (StreamWriter sw = new StreamWriter(SavePaths)) { };
        }

        public static bool PathCheckSource()
        {
            bool result = true;
            string path = "";
            using StreamReader sr = new StreamReader(SavePaths);
            {
                for (int i = 0; i < 2; i++)
                {
                    if (i == 1)
                    {
                        path = sr.ReadLine();
                    }
                    else
                        sr.ReadLine();
                }
            }

            if (!path.Contains(@":\"))
                result = false;
            return result;
        }

        public static bool PathCheckDest()
        {
            bool result = true;
            string path = "";
            using StreamReader sr = new StreamReader(SavePaths);
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i == 2)
                    {
                        path = sr.ReadLine();
                    }
                    else
                        sr.ReadLine();
                }
            }

            if (!path.Contains(@":\"))
                result = false;
            return result;
        }

        public static void PathUpdateFile()
        {
            using StreamWriter sw = new StreamWriter(SavePaths);
            {
                for (int i = 0; i < Paths.AllPaths.Length; i++)
                {
                    sw.WriteLine(Paths.AllPaths[i]);
                }
            }
        }

        public static void GetPaths()
        {
            using StreamReader sr = new StreamReader(SavePaths);
            {
                for (int i = 0; i < Paths.AllPaths.Length; i++)
                {
                    Paths.AllPaths[i] = sr.ReadLine();
                }
            }
        }
    }
}
