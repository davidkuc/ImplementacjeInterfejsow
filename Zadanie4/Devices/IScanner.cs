public interface IScanner : IDevice
{
    public State State { get; }

    /// <summary>
    /// Skanuje dokument i konwertuje w zależności od wprowadzonego typu
    /// </summary>
    /// <param name="document">Dokument różny od 'null'</param>
    void Scan(out IDocument document, IDocument.FormatType formatType);
              

    /// <summary>
    /// Skanuje dokument i konwertuje w domyślny format .txt
    /// </summary>
    /// <param name="document">Dokument różny od 'null'</param>
    void Scan(out IDocument document);
}