using Zadanie2.Devices;

public class MultifunctionalDevice : Zadanie2.Devices.MultifunctionalDevice, IFax
{
    public int SendCounter { get; protected set; }

    public void Send(in IDocument document)
    {
        Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName}.txt");
    }

     public void Scan(out IDocument document)
    {
        var fileName = string.Empty;

        if (state != IDevice.State.on)
        {
            fileName = $"ImageScan{ScanCounter}";
            document = new ImageDocument(fileName);
            return;
        }
        fileName = $"ImageScan{ScanCounter}";
        document = new ImageDocument(fileName);
        Console.WriteLine($"{DateTime.Now} Scan: {fileName}.txt");
        ScanCounter++;
    }
}