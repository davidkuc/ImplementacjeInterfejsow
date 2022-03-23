using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3.Devices
{
    public interface ICopier
    {
        public int ScanCounter { get; }
        public int PrintCounter { get; }

        void Scan(out IDocument document, IDocument.FormatType formatType);

        void Scan(out IDocument document);

        void Print(in IDocument document);

        void ScanAndPrint();

        void ScannerPowerOn();

        void ScannerPowerOff();

        void PrinterPowerOn();

        void PrinterPowerOff();

        void AllDevicesPowerOn();

        void AllDevicesPowerOff();
    }
}
