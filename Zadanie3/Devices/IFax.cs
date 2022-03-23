public interface IFax : IDevice
{
    public int SendCounter { get; }

    /// <summary>
    /// "Wysyła" plik do celu, w tym przypadku drukuje do konsoli dane dokumentu
    /// </summary>
    /// <param name="document">Dokument różny od 'null'</param>
    void Send(in IDocument document);
}
