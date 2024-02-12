using StructureLogging.Models;

namespace StructureLogging.Services;

public interface IDeviceService
{
    IEnumerable<Device> GetDevices(DeviceTypes deviceType);
}

public class DeviceService : IDeviceService
{
    #region Init

    private readonly ILogger<DeviceService> _logger;

    public DeviceService(ILogger<DeviceService> logger)
    {
        _logger = logger;
    }

    #endregion

    public IEnumerable<Device> GetDevices(DeviceTypes deviceType)
    {
        switch (deviceType)
        {
            case DeviceTypes.Main:
                return GetMainDevices();
            case DeviceTypes.Additional:
                return GetAdditionalDevices();
            default:
                _logger.LogWarning("Attempt to get devices with Type = {DeviceType}", deviceType);
                return Array.Empty<Device>();
        }
    }

    #region Private

    private static List<Device> GetMainDevices()
    {
        return new List<Device>
        {
            new() { Type = DeviceTypes.Main, Name = "Main 1" },
            new() { Type = DeviceTypes.Main, Name = "Main 2" },
            new() { Type = DeviceTypes.Main, Name = "Main 3" },
        };
    }

    private static List<Device> GetAdditionalDevices()
    {
        return new List<Device>
        {
            new() { Type = DeviceTypes.Additional, Name = "Additional 1" },
            new() { Type = DeviceTypes.Additional, Name = "Additional 2" },
            new() { Type = DeviceTypes.Additional, Name = "Additional 3" },
        };
    }

    #endregion
}
