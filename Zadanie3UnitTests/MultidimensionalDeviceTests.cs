using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Zadanie3.Devices;

namespace Zadanie3UnitTests
{
    [TestClass]
    public class UnitTestMultidimensionalDevice
    {

        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOff()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, multidimensionalDevice.GetState());
        }

        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOn()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, multidimensionalDevice.GetState());
        }

        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOn()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multidimensionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOff()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                 (
                 new Printer(),
                 new Scanner(),
                 new Fax()
                 );
            multidimensionalDevice.PowerOff();
            multidimensionalDevice.AllDevicesPowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multidimensionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOff()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                 (
                 new Printer(),
                 new Scanner(),
                 new Fax()
                 );
            multidimensionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multidimensionalDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOn()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multidimensionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultidimensionalDevice_Scan_FormatTypeDocument()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multidimensionalDevice.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                multidimensionalDevice.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                multidimensionalDevice.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOn()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multidimensionalDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOff()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multidimensionalDevice.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_PrintCounter()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                 (
                 new Printer(),
                 new Scanner(),
                 new Fax()
                 );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multidimensionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multidimensionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            multidimensionalDevice.Print(in doc3);

            multidimensionalDevice.PowerOff();
            multidimensionalDevice.Print(in doc3);
            multidimensionalDevice.Scan(out doc1);
            multidimensionalDevice.PowerOn();

            multidimensionalDevice.ScanAndPrint();
            multidimensionalDevice.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, multidimensionalDevice.PrintCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_ScanCounter()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            IDocument doc1;
            multidimensionalDevice.Scan(out doc1);
            IDocument doc2;
            multidimensionalDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multidimensionalDevice.Print(in doc3);

            multidimensionalDevice.PowerOff();
            multidimensionalDevice.Print(in doc3);
            multidimensionalDevice.Scan(out doc1);
            multidimensionalDevice.PowerOn();

            multidimensionalDevice.ScanAndPrint();
            multidimensionalDevice.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, multidimensionalDevice.ScanCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_PowerOnCounter()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.PowerOn();

            IDocument doc1;
            multidimensionalDevice.Scan(out doc1);
            IDocument doc2;
            multidimensionalDevice.Scan(out doc2);

            multidimensionalDevice.PowerOff();
            multidimensionalDevice.PowerOff();
            multidimensionalDevice.PowerOff();
            multidimensionalDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multidimensionalDevice.Print(in doc3);

            multidimensionalDevice.PowerOff();
            multidimensionalDevice.Print(in doc3);
            multidimensionalDevice.Scan(out doc1);
            multidimensionalDevice.PowerOn();

            multidimensionalDevice.ScanAndPrint();
            multidimensionalDevice.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, multidimensionalDevice.Counter);
        }

        [TestMethod]
        public void MultidimensionalDevice_Scan_PowerOff()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multidimensionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput() == string.Empty);
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_Scan_PowerOn()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multidimensionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_Send_PowerOn()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                 (
                 new Printer(),
                 new Scanner(),
                 new Fax()
                 );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new ImageDocument("aaa.jpg");
                multidimensionalDevice.Send(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("File sent"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Image"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_Send_PowerOff()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new ImageDocument("aaa.jpg");
                multidimensionalDevice.Send(doc1);
                Assert.IsTrue(!consoleOutput.GetOutput().Contains("File sent"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_SendCounter()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            IDocument doc1 = new ImageDocument("aaa.jpg");
            IDocument doc2 = new ImageDocument("aaa.jpg");
            IDocument doc3 = new ImageDocument("aaa.jpg");
            IDocument doc4 = new PDFDocument("aaa.pdf");
            multidimensionalDevice.Send(doc1);
            multidimensionalDevice.Send(doc2);
            multidimensionalDevice.Send(doc3);
            multidimensionalDevice.Send(doc4);
            multidimensionalDevice.AllDevicesPowerOff();
            multidimensionalDevice.PowerOff();

            IDocument doc5 = new TextDocument("aaa.txt");
            IDocument doc6 = new ImageDocument("aaa.jpg");
            multidimensionalDevice.Send(doc5);
            multidimensionalDevice.Send(doc6);
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            IDocument doc7 = new ImageDocument("aaa.jpg");
            IDocument doc8 = new TextDocument("aaa.txt");
            multidimensionalDevice.Send(doc7);
            multidimensionalDevice.Send(doc8);

            Assert.AreEqual(multidimensionalDevice.SendCounter, 4);
        }

        [TestMethod]
        public void MultidimensionalDevice_Send_TypeValidation()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multidimensionalDevice.Send(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("File is not an image type (.jpg)."));
                consoleOutput.Flush();

                IDocument doc2 = new TextDocument("aaa.txt");
                multidimensionalDevice.Send(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("File is not an image type (.jpg)."));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_Send_TypeConversion()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();

            IDocument doc;
            multidimensionalDevice.Scan(out doc);

            Assert.IsTrue(Equals(doc.GetType(), typeof(ImageDocument)));
        }

        [TestMethod]
        public void MultidimensionalDevice_AllDevicesPowerOn()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );

            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOn();
            var devicesStates = multidimensionalDevice.GetDevicesStates();

            Assert.IsTrue(multidimensionalDevice.GetState() == IDevice.State.on);
            Assert.IsTrue(devicesStates["Printer"] == IDevice.State.on);
            Assert.IsTrue(devicesStates["Scanner"] == IDevice.State.on);
            Assert.IsTrue(devicesStates["Fax"] == IDevice.State.on);
        }

        [TestMethod]
        public void MultidimensionalDevice_AllDevicesPowerOff()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );

            multidimensionalDevice.PowerOn();
            multidimensionalDevice.AllDevicesPowerOff();
            var devicesStates = multidimensionalDevice.GetDevicesStates();

            Assert.IsTrue(multidimensionalDevice.GetState() == IDevice.State.on);
            Assert.IsTrue(devicesStates["Printer"] == IDevice.State.off);
            Assert.IsTrue(devicesStates["Scanner"] == IDevice.State.off);
            Assert.IsTrue(devicesStates["Fax"] == IDevice.State.off);
        }

        [TestMethod]
        public void MultidimensionalDevice_PowerOn()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );

            multidimensionalDevice.PowerOn();


            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multidimensionalDevice.AllDevicesPowerOn();
                Assert.IsTrue(!consoleOutput.GetOutput().Contains($"turned off"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_PowerOff()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multidimensionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multidimensionalDevice.AllDevicesPowerOn();
                Assert.IsTrue(consoleOutput.GetOutput().Contains($" is turned off."));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_DevicesPowerOn()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );

            multidimensionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multidimensionalDevice.PrinterPowerOn();
                Assert.IsTrue(consoleOutput.GetOutput().Contains($"turned on"));
                consoleOutput.Flush();

                multidimensionalDevice.ScannerPowerOn();
                Assert.IsTrue(consoleOutput.GetOutput().Contains($"turned on"));
                consoleOutput.Flush();

                multidimensionalDevice.FaxPowerOn();
                Assert.IsTrue(consoleOutput.GetOutput().Contains($"turned on"));
                consoleOutput.Flush();
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_DevicesPowerOff()
        {
            var multidimensionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );

            multidimensionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multidimensionalDevice.PrinterPowerOff();
                Assert.IsTrue(consoleOutput.GetOutput().Contains($"turned off"));
                consoleOutput.Flush();

                multidimensionalDevice.ScannerPowerOff();
                Assert.IsTrue(consoleOutput.GetOutput().Contains($"turned off"));
                consoleOutput.Flush();

                multidimensionalDevice.FaxPowerOff();
                Assert.IsTrue(consoleOutput.GetOutput().Contains($"turned off"));
                consoleOutput.Flush();
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
    }
}