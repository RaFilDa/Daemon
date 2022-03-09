using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupAlgs.Tools;

namespace BackupAlgs.Backup
{
    internal class Full : IBackup
    {
        public string PathSource { get; set; }
        public string PathDestination { get; set; }

        public Full()
        {
            PathSource = Paths.PathSource;
            PathDestination = Paths.PathDest;
        }
    }
}
