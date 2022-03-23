namespace Zadanie3.Devices
{
    public interface IMultidimensionalDevice
    {
        public int ScanCounter { get; }
        public int PrintCounter { get; }
        public int SendCounter { get; }

        void Scan(out IDocument document, IDocument.FormatType formatType);

        void Scan(out IDocument document);

        void Print(in IDocument document);

        void ScanAndPrint();

        void ScannerPowerOn();

        void ScannerPowerOff();

        void PrinterPowerOn();

        void PrinterPowerOff();

        void FaxPowerOn();

        void FaxPowerOff();

        void AllDevicesPowerOn();

        void AllDevicesPowerOff();

        Dictionary<string, IDevice.State> GetDevicesStates();


    }
}