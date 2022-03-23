public abstract class BaseDevice : IDevice
{
    protected IDevice.State state = IDevice.State.off;
    public IDevice.State GetState() => state;

    public void PowerOff()
    {
        state = IDevice.State.off;
        Console.WriteLine($"... {this.GetType().Name} is off !");
    }

    public void PowerOn()
    {
        if (state != IDevice.State.on)
        {
            Counter++;
        }

        state = IDevice.State.on;
        Console.WriteLine($"{this.GetType().Name} is on ...");
    }

    public int Counter { get; private set; } = 0;
}
