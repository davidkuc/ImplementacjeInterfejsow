public interface IPrinter : IDevice
{
    public State State { get; }

    /// <summary>
    /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
    /// </summary>
    /// <param name="document">obiekt typu IDocument, różny od `null`</param>
    void Print(in IDocument document) => Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName}");
    
}
