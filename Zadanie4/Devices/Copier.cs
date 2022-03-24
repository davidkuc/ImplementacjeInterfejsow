using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie4.Devices
{
    public class Copier : ICopier
    {
        private readonly IPrinter _printer;
        private readonly IScanner _scanner;

        public IDevice.State State { get; set; }
        public int PrintCounter { get; private set; }
        public int ScanCounter { get; private set; }
        public int Counter { get; private set; }

        public Copier(IPrinter printer
                , IScanner scanner)
        {
            _printer = printer;
            _scanner = scanner;
        }

        public IDevice.State GetState()
        {
            return State;
        }

        void IDevice.SetState(IDevice.State state)
        {
            State = state;
        }

        public void Print(in IDocument document)
        {
            if (State != IDevice.State.on)
            {
                return;
            }

            if (_printer.State == IDevice.State.standby)
            {
                _printer.StandbyOff();
            }

            _scanner.StandbyOn();
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName}");
            PrintCounter++;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            var fileName = string.Empty;

            if (State != IDevice.State.on)
            {
                fileName = $"TextScan{ScanCounter}";
                document = new TextDocument(fileName);
                return;
            }

            if (_scanner.State == IDevice.State.standby)
            {
                _scanner.StandbyOff();
            }

            _printer.StandbyOn();
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    fileName = $"PDFScan{ScanCounter}";
                    document = new PDFDocument(fileName);
                    Console.WriteLine($"{DateTime.Now} Scan: {fileName}.pdf");
                    break;
                case IDocument.FormatType.TXT:
                    fileName = $"TextScan{ScanCounter}.txt";
                    document = new TextDocument(fileName);
                    Console.WriteLine($"{DateTime.Now} Scan: {fileName}");
                    break;
                case IDocument.FormatType.JPG:
                    fileName = $"ImageScan{ScanCounter}.jpg";
                    document = new ImageDocument(fileName);
                    Console.WriteLine($"{DateTime.Now} Scan: {fileName}");
                    break;
                default:
                    fileName = $"Image{ScanCounter}.jpg";
                    document = new ImageDocument(fileName);
                    Console.WriteLine($"{DateTime.Now} Scan: {fileName}");
                    break;
            }
            ScanCounter++;
        }

        public void Scan(out IDocument document)
        {
            var fileName = string.Empty;

            if (State != IDevice.State.on)
            {
                document = null;
                return;
            }

            if (_scanner.State == IDevice.State.standby)
            {
                _scanner.StandbyOff();
            }

            _printer.StandbyOn();
            fileName = $"ImageScan{ScanCounter}.jpg";
            document = new TextDocument(fileName);
            Console.WriteLine($"{DateTime.Now} Scan: {fileName}");
            ScanCounter++;
        }

        public void ScanAndPrint()
        {
            if (State != IDevice.State.on)
            {
                return;
            }

            IDocument document;
            Scan(out document, IDocument.FormatType.JPG);
            Print(document);
        }

    }

}

