using Zadanie3.Devices;

var multidimensionalDevice = new MultidimensionalDevice
    (
    new Printer(),
    new Scanner(),
    new Fax()
    );

multidimensionalDevice.PowerOn();
multidimensionalDevice.ScannerPowerOn();

IDocument doc;
multidimensionalDevice.Scan(out doc);
multidimensionalDevice.Print(doc);

multidimensionalDevice.PrinterPowerOn();
multidimensionalDevice.Print(doc);

multidimensionalDevice.AllDevicesPowerOff();
multidimensionalDevice.AllDevicesPowerOn();
multidimensionalDevice.AllDevicesPowerOff();

multidimensionalDevice.Send(doc);
multidimensionalDevice.FaxPowerOn();
multidimensionalDevice.Send(doc);

Console.WriteLine();
Console.WriteLine($"Scan count: {multidimensionalDevice.ScanCounter}");
Console.WriteLine($"Print count: {multidimensionalDevice.PrintCounter}");
Console.WriteLine($"Send count: {multidimensionalDevice.SendCounter}");
