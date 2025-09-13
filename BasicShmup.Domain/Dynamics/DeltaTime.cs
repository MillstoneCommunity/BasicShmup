namespace BasicShmup.Domain.Dynamics;

public struct DeltaTime
{
    #region static constructors

    public static DeltaTime FromSeconds(double deltaTimeInSeconds)
    {
        return new DeltaTime
        {
            _deltaTime = TimeSpan.FromSeconds(deltaTimeInSeconds)
        };
    }

    #endregion

    private TimeSpan _deltaTime;

    public float Seconds => (float)_deltaTime.TotalSeconds;
}
