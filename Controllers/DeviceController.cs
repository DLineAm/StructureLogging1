using Microsoft.AspNetCore.Mvc;

using StructureLogging.Models;
using StructureLogging.Services;

namespace StructureLogging.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpGet]
    public ActionResult<List<Device>> Get([FromQuery] DeviceTypes deviceType)
    {
        var devices = _deviceService.GetDevices(deviceType).ToList();

        if(devices.Count == 0)
        {
            return NotFound();
        }

        return Ok(devices);
    }
}
