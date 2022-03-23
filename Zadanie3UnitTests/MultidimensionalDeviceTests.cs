using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Zadanie3.Devices;

namespace Zadanie3UnitTests
{
    [TestClass]
    public class UnitTestMultidimensionalDevice
    {
       
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(), 
                new Scanner(), 
                new Fax()
                );
            multifunctionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, multifunctionalDevice.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, multifunctionalDevice.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.AllDevicesPowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                 (
                 new Printer(),
                 new Scanner(),
                 new Fax()
                 );
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                 (
                 new Printer(),
                 new Scanner(),
                 new Fax()
                 );
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_PrintCounter()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                 (
                 new Printer(),
                 new Scanner(),
                 new Fax()
                 );
            multifunctionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multifunctionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, multifunctionalDevice.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanCounter()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, multifunctionalDevice.ScanCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, multifunctionalDevice.Counter);
        }

        [TestMethod]
        public void MultifunctionalDevice_Scan_PowerOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput() == string.Empty);
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Scan_PowerOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Send_PowerOn()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                 (
                 new Printer(),
                 new Scanner(),
                 new Fax()
                 );
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new ImageDocument("aaa.jpg");
                multifunctionalDevice.Send(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("File sent"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Image"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Send_PowerOff()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new ImageDocument("aaa.jpg");
                multifunctionalDevice.Send(doc1);
                Assert.IsTrue(consoleOutput.GetOutput() == string.Empty);
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_SendCounter()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOn();

            IDocument doc1 = new ImageDocument("aaa.jpg");
            IDocument doc2 = new ImageDocument("aaa.jpg");
            IDocument doc3 = new ImageDocument("aaa.jpg");
            IDocument doc4 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.Send(doc1);
            multifunctionalDevice.Send(doc2);
            multifunctionalDevice.Send(doc3);
            multifunctionalDevice.Send(doc4);
            multifunctionalDevice.PowerOff();

            IDocument doc5 = new TextDocument("aaa.txt");
            IDocument doc6 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Send(doc5);
            multifunctionalDevice.Send(doc6);
            multifunctionalDevice.PowerOn();

            IDocument doc7 = new ImageDocument("aaa.jpg");
            IDocument doc8 = new TextDocument("aaa.txt");
            multifunctionalDevice.Send(doc7);
            multifunctionalDevice.Send(doc8);

            Assert.AreEqual(multifunctionalDevice.SendCounter, 4);
        }

        [TestMethod]
        public void MultifunctionalDevice_Send_TypeValidation()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Send(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("File is not an image type (.jpg)."));
                consoleOutput.Flush();

                IDocument doc2 = new TextDocument("aaa.txt");
                multifunctionalDevice.Send(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("File is not an image type (.jpg)."));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Send_TypeConversion()
        {
            var multifunctionalDevice = new MultidimensionalDevice
                (
                new Printer(),
                new Scanner(),
                new Fax()
                );
            multifunctionalDevice.PowerOn();

            IDocument doc;
            multifunctionalDevice.Scan(out doc);

            Assert.IsTrue(Equals(doc.GetType(), typeof(ImageDocument)));
        }
    }
}