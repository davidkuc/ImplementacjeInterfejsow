namespace Zadanie3.Devices
{
    public class Scanner : BaseDevice, IScanner
    {
        public int ScanCounter { get; protected set; }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            var fileName = string.Empty;

            if (state != IDevice.State.on)
            {
                Console.WriteLine($"{this.GetType().Name} is turned off - cannot perform any action");
                document = null;
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
                Console.WriteLine($"{this.GetType().Name} is turned off - cannot perform any action");
                document = null;
                return;
            }
            fileName = $"ImageScan{ScanCounter}.jpg";
            document = new ImageDocument(fileName);
            Console.WriteLine($"{DateTime.Now} Scan: {fileName}.jpg");
            ScanCounter++;
        }
    }
}
