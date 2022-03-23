public class Fax : BaseDevice, IFax
{
    public int SendCounter { get; protected set; }

    public void Send(in IDocument document)
    {
        if (state != IDevice.State.on)
        {
            Console.WriteLine($"{this.GetType().Name} is turned off - cannot perform any action");
            return;
        }

        if (!document.GetFileName().Contains(".jpg"))
        {
            Console.WriteLine("File is not an image type (.jpg).");
            return;
        }
        Console.WriteLine($"File sent - {DateTime.Now} Image: {document.GetFileName()}.jpg");
        SendCounter++;
    }
}
