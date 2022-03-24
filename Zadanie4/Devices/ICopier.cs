using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie4.Devices
{
    public interface ICopier : IPrinter, IScanner
    {
        public int PrintCounter { get; }
        public int ScanCounter { get; }

        void ScanAndPrint();
    }
}
