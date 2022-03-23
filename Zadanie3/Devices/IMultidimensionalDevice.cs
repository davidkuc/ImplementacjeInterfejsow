namespace Zadanie3.Devices
{
    public interface IMultidimensionalDevice
    {
        public int SendCounter { get; }

        void Send(in IDocument document);

        
    }
}