﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.Devices
{
    public class Copier : BaseDevice, ICopier
    {
        public int PrintCounter { get; private set; }
        public int ScanCounter { get; private set; }

        public void Print(in IDocument document)
        {
            if (state != IDevice.State.on)
            {
                return;
            }

            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName}");
            PrintCounter++;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            var fileName = String.Empty;

            if (state != IDevice.State.on)
            {
                fileName = $"NoType{ScanCounter}";
                document = new TextDocument(fileName);
                return;
            }

            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    fileName = $"PDFScan{ScanCounter}";
                    document = new PDFDocument(fileName);
                    Console.WriteLine($"{DateTime.Now} Scan: {fileName}.pdf");
                    break;
                case IDocument.FormatType.TXT:
                    fileName = $"TextScan{ScanCounter}";
                    document = new TextDocument(fileName);
                    Console.WriteLine($"{DateTime.Now} Scan: {fileName}.txt");
                    break;
                case IDocument.FormatType.JPG:
                    fileName = $"ImageScan{ScanCounter}";
                    document = new ImageDocument(fileName);
                    Console.WriteLine($"{DateTime.Now} Scan: {fileName}.jpg");
                    break;
                default:
                    fileName = $"NoType{ScanCounter}";
                    document = new TextDocument(fileName);
                    Console.WriteLine($"{DateTime.Now} Scan: {fileName}.txt");
                    break;
            }
            ScanCounter++;
        }

        public void Scan(out IDocument document)
        {
            var fileName = String.Empty;

            if (state != IDevice.State.on)
            {
                fileName = $"NoType{ScanCounter}";
                document = new TextDocument(fileName);
                return;
            }
            document = new PDFDocument(fileName);
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

