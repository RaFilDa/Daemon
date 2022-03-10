using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupAlgs.Tools;
using BackupAlgs.Windows;

namespace BackupAlgs
{
    public class Application
    {
        public Window ActiveWindow { get; set; }
        public bool IsOn { get; set; }

        public Application(string title)
        {
            IsOn = true;
            ActiveWindow = new MainWindow(this, title);

            if (!PathTools.PathFileExists())
            {
                PathTools.PathFileCreate();
                PathTools.PathUpdateFile();
            }
            else
                PathTools.GetPaths();

            if (!LogTools.LogFileExists())
            {
                LogTools.LogFileCreate();
            }
            else
                LogTools.GetLogs();
        }
    }
}
