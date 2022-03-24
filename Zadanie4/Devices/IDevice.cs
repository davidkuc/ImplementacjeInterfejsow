public interface IDevice
{
    enum State { on, off, standby };

    void PowerOn() => SetState(State.on); // uruchamia urządzenie, zmienia stan na `on`

    void PowerOff() => SetState(State.off); // wyłącza urządzenie, zmienia stan na `off

    State GetState(); // zwraca aktualny stan urządzenia

    void StandbyOn() => SetState(State.standby);

    void StandbyOff() => SetState(State.on);

    abstract protected void SetState(State state);

    int Counter { get; }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                          // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
}
