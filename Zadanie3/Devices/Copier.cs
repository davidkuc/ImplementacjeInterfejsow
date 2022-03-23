using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3.Devices
{
    public class Copier : BaseDevice, ICopier
    {
        private readonly IPrinter _printer;
        private readonly IScanner _scanner;

        public int ScanCounter => _scanner.ScanCounter;
        public int PrintCounter => _printer.PrintCounter;

        public Copier(IPrinter printer
            ,IScanner scanner)
        {
            _printer = printer;
            _scanner = scanner;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {

            if (state != IDevice.State.on)
            {
                document = null;
                return;
            }
            _scanner.Scan(out document, formatType); 
        }

        public void Scan(out IDocument document)
        {
            if (state != IDevice.State.on)
            {
                document = null;
                return;
            }

            _scanner.Scan(out document);
        }

        public void Print(in IDocument document)
        {
            if (state != IDevice.State.on)
            {
                return;
            }

            _printer.Print(document);
        }

        public void ScanAndPrint()
        {
            if (state != IDevice.State.on)
            {
                return;
            }

            IDocument document;
            Scan(out document);
            Print(document);
        }

        public void ScannerPowerOn()
        {
            _scanner.PowerOn();
        }

        public void ScannerPowerOff()
        {
            _scanner.PowerOff();
        }

        public void PrinterPowerOn()
        {
            _printer.PowerOn();
        }

        public void PrinterPowerOff()
        {
            _printer.PowerOff();
        }

        public virtual void AllDevicesPowerOn()
        {
            _printer.PowerOn();
            _scanner.PowerOn();
        }

        public virtual void AllDevicesPowerOff()
        {
            _printer.PowerOff();
            _scanner.PowerOff();
        }

        public virtual Dictionary<string, IDevice.State> GetDevicesStates()
        {
            var devicesStates = new Dictionary<string, IDevice.State>()
            {
                {"Printer", _printer.GetState() },
                {"Scanner", _scanner.GetState() }
            };

            return devicesStates;
        }
    }
}

