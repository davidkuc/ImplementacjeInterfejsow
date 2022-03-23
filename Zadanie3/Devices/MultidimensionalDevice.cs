namespace Zadanie3.Devices
{
    public class MultidimensionalDevice : Copier, IMultidimensionalDevice
    {
        private readonly IFax _fax;

        public MultidimensionalDevice(IPrinter printer
            , IScanner scanner
            , IFax fax) : base(printer, scanner)
        {
            _fax = fax;
        }

        public int SendCounter  => _fax.SendCounter;

        public void Send(in IDocument document)
        {
            _fax.Send(document);
        }

        public override void AllDevicesPowerOn()
        {
            base.AllDevicesPowerOn();
            _fax.PowerOn();
        }

        public override void AllDevicesPowerOff()
        {
            base.AllDevicesPowerOff();
            _fax.PowerOff();
        }

        public override Dictionary<string, IDevice.State> GetDevicesStates()
        {
            var devicesStates = base.GetDevicesStates();
            devicesStates.Add("Fax", _fax.GetState());

            return devicesStates;
        }
    }
}
