var multifunctionalDevice = new MultifunctionalDevice();
multifunctionalDevice.PowerOn();

IDocument doc1;
multifunctionalDevice.Scan(out doc1);
multifunctionalDevice.Send(doc1);

IDocument doc2;
multifunctionalDevice.Scan(out doc2, IDocument.FormatType.PDF);
multifunctionalDevice.Print(doc2);


Console.WriteLine();
Console.WriteLine($"Scan count: {multifunctionalDevice.ScanCounter}");
Console.WriteLine($"Print count: {multifunctionalDevice.PrintCounter}");
Console.WriteLine($"Send count: {multifunctionalDevice.SendCounter}");