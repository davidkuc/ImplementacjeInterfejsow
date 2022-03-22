public interface IScanner : IDevice
{
    // dokument jest skanowany, jeśli urządzenie włączone
    // w przeciwnym przypadku nic się dzieje
    void Scan(IDocument.FormatType formatType);
}