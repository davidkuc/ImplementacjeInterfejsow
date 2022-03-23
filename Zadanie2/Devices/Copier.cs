using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2.Devices
{
    public class Copier : BaseDevice, ICopier
    {
        public int PrintCounter { get; protected set; }
        public int ScanCounter { get; protected set; }

        public void Print(in IDocument document)
        {
            if (state != IDevice.State.on)
            {
                return;
            }

            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            PrintCounter++;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            var fileName = string.Empty;

            if (state != IDevice.State.on)
            {
                fileName = $"TextScan{ScanCounter}";
                document = new TextDocument(fileName);
                return;
            }

            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    fileName = $"PDFScan{ScanCounter}.pdf";
                    document = new PDFDocument(fileName);
                    Console.WriteLine($"{DateTime.Now} Scan: {fileName}");
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

            if (state != IDevice.State.on)
            {
                document = null;
                return;
            }
            fileName = $"ImageScan{ScanCounter}.jpg";
            document = new ImageDocument(fileName);
            Console.WriteLine($"{DateTime.Now} Scan: {fileName}.jpg");
            ScanCounter++;
        }

        public void ScanAndPrint()
        {
            if (state != IDevice.State.on)
            {
                return;
            }

            IDocument document;
            Scan(out document, IDocument.FormatType.JPG);
            Print(document);
        }
    }
}

