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

        public void FaxPowerOn()
        {
            if (state != IDevice.State.on)
            {
                Console.WriteLine($"{this.GetType().Name} is turned off.");
                return;
            }

            _fax.PowerOn();
            Console.WriteLine($"{_fax.GetType().Name} has been turned on.");
        }

        public void FaxPowerOff()
        {
            if (state != IDevice.State.on)
            {
                Console.WriteLine($"{this.GetType().Name} is turned off.");
                return;
            }

            _fax.PowerOff();
            Console.WriteLine($"{_fax.GetType().Name} has been turned off.");
        }

        public override void AllDevicesPowerOn()
        {
            if (state != IDevice.State.on)
            {
                Console.WriteLine($"{this.GetType().Name} is turned off.");
                return;
            }

            base.AllDevicesPowerOn();
            _fax.PowerOn();
            Console.WriteLine($"{_fax.GetType().Name} has been turned on.");

        }

        public override void AllDevicesPowerOff()
        {
            if (state != IDevice.State.on)
            {
                Console.WriteLine($"{this.GetType().Name} is turned off.");
                return;
            }

            base.AllDevicesPowerOff();
            _fax.PowerOff();
            Console.WriteLine($"{_fax.GetType().Name} has been turned off.");
        }

        public override Dictionary<string, IDevice.State> GetDevicesStates()
        {
            if (state != IDevice.State.on)
            {
                Console.WriteLine($"{this.GetType().Name} is turned off.");
                return null;
            }

            var devicesStates = base.GetDevicesStates();
            devicesStates.Add("Fax", _fax.GetState());

            return devicesStates;
        }
    }
}
