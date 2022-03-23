namespace Zadanie3.Devices
{
    public class Printer : BaseDevice, IPrinter
    {
        public int PrintCounter { get; protected set; }

        public void Print(in IDocument document)
        {
            if (state != IDevice.State.on)
            {
                return;
            }

            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            PrintCounter++;
        }
    }
}
