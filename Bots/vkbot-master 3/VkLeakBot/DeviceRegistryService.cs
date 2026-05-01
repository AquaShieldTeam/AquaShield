using System.Collections.Concurrent;

public class DeviceRegistryService
{
    // Ключ: "hubId:sensorId" → VK userId
    private readonly ConcurrentDictionary<string, long> _deviceToUser = new();

    public void RegisterDevice(string hubId, string sensorId, long vkUserId)
    {
        var key = $"{hubId}:{sensorId}".ToLowerInvariant();
        _deviceToUser[key] = vkUserId;
    }

    public long? GetUserByDevice(string hubId, string sensorId)
    {
        var key = $"{hubId}:{sensorId}".ToLowerInvariant();
        return _deviceToUser.TryGetValue(key, out var userId) ? userId : null;
    }

    // Для отладки
    public IReadOnlyDictionary<string, long> GetAllBindings() => _deviceToUser;
}