public abstract class BaseDevice : IDevice
{
    protected IDevice.State state = IDevice.State.off;
    public IDevice.State GetState() => state;

    public void PowerOff()
    {
        state = IDevice.State.off;
        Console.WriteLine($"... {this.GetType().Name} is turned off !");
    }

    public void PowerOn()
    {
        if (state != IDevice.State.on)
        {
            Counter++;
        }

        state = IDevice.State.on;
        Console.WriteLine($"{this.GetType().Name} is turned on ...");
    }

    public int Counter { get; private set; } = 0;
}
