using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BackupAlgs.Tools;
using BackupAlgs.Windows;

namespace BackupAlgs
{
    public class Application
    {
        public Window ActiveWindow { get; set; }
        public string DefaultPath = @"..\paths.txt";
        public bool IsOn { get; set; }

        public Application(string title)
        {
            IsOn = true;
            ActiveWindow = new MainWindow(title);

            if (!File.Exists(DefaultPath))
                using (StreamWriter sw = new StreamWriter(DefaultPath)) { };
            PathTools.PathUpdateFile();
        }
    }
}
