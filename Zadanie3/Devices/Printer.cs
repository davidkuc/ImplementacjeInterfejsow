namespace Zadanie3.Devices
{
    public class Printer : BaseDevice, IPrinter
    {
        public int PrintCounter { get; protected set; }

        public void Print(in IDocument document)
        {
            if (state != IDevice.State.on)
            {
                Console.WriteLine($"{this.GetType().Name} is turned off - cannot perform any action");
                return;
            }

            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            PrintCounter++;
        }
    }
}
