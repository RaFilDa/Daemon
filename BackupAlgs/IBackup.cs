using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupAlgs
{
    public interface IBackup
    {
        public string PathSource { get; set; }
        public string PathDestination { get; set; }
    }
}
