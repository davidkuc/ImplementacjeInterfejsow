public interface IFax : IDevice
{

    /// <summary>
    /// Skanuje dokument i konwertuje w obraz .jpg
    /// </summary>
    /// <param name="document">Dokument różny od 'null'</param>
    void Scan(out IDocument document);

    /// <summary>
    /// "Wysyła" plik do celu, w tym przypadku drukuje do konsoli dane dokumentu
    /// </summary>
    /// <param name="document">Dokument różny od 'null'</param>
    void Send(in IDocument document);
}
