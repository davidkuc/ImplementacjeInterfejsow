using Zadanie2.Devices;

public class MultifunctionalDevice : Copier, IFax
{
    public int SendCounter { get; protected set; }

    public void Send(in IDocument document)
    {
        if (state != IDevice.State.on)
        {
            return;
        }
        Console.WriteLine($"File sent - {DateTime.Now} Image: {document.GetFileName()}.jpg");
        SendCounter++;
    }

     new public void Scan(out IDocument document)
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
        Console.WriteLine($"{DateTime.Now} Scan: {fileName}.jpg");
        ScanCounter++;
    }
}