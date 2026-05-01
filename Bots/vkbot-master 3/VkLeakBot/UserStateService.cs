using System.Collections.Concurrent;

public class UserSession
{
    public string CurrentState { get; set; } = "ready";
    public long? HubId { get; set; }
    public long? SensorId { get; set; }
    public string? Location { get; set; }
    public int? WaterThreshold { get; set; }
    public bool? BatteryThreshold { get; set; } // true = 50%, false = 20%
    public int? Notifications { get; set; } // 0,1,2
    public bool? Shutoff { get; set; }
    public string? NewLocation { get; set; }
    public int? NewNotifications { get; set; }
    public bool? NewShutoff { get; set; }
    public int? CurrentPage { get; set; } = 0;
}

public class UserStateService
{
    private readonly ConcurrentDictionary<long, UserSession> _sessions = new();

    public Task<UserSession> GetState(long userId)
    {
        return Task.FromResult(_sessions.GetOrAdd(userId, _ => new UserSession()));
    }

    public Task UpdateState(long userId, UserSession session)
    {
        _sessions[userId] = session;
        return Task.CompletedTask;
    }
}