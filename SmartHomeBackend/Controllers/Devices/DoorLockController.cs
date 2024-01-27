using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using SmartHomeBackend.Globals;
using SmartHomeBackend.Models;
using SmartHomeBackend.Models.Dto;
using SmartHomeBackend.Services;
using System.Text.Json;

namespace SmartHomeBackend.Controllers.Devices
{
    [Route("api/system/{systemId}/board/{boardId}/devices/door-locks")]
    [ApiController]
    public class DoorLockController : ControllerBase
    {
        private readonly SmartHomeDbContext _context;
        private readonly DeviceService _deviceService;

        public DoorLockController(DeviceService deviceService, SmartHomeDbContext context)
        {
            _deviceService = deviceService;
            _context = context;
        }

        [Route("states")]
        [HttpGet]
        public async Task<IActionResult> GetAllDoorLocksStates()
        {
            try
            {
                string url = $"{Strings.RPI_API_URL_ADRIAN}/door-locks/states";
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<DoorLockStateDto[]>(text);
                    foreach (var doorLock in array ?? [])
                    {
                        var doorLockInDB = _context.DoorLocks.Find(doorLock.doorLock_Id.ToString());
                        if(doorLockInDB != null)
                            doorLockInDB.IsOn = doorLock.isOn;
                    }
                    _context.SaveChanges();

                    return Ok(_context.DoorLocks);
                }
                else
                {
                    throw new Exception("Couldn't get all door locks states.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("states/{doorLockId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneDoorLockState(int doorLockId)
        {
            try
            {
                string url = $"{Strings.RPI_API_URL_ADRIAN}/door-locks/states/{doorLockId}";
                
                if (_context.DoorLocks.Find(doorLockId.ToString()) == null)
                    throw new Exception($"Door Lock with id {doorLockId} not found in database.");

                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var doorLock = JsonSerializer.Deserialize<DoorLockStateDto>(text);
                    var doorLockInDB = _context.DoorLocks.Find(doorLockId.ToString());

                    if (doorLockInDB != null && doorLock != null)
                    {
                        doorLockInDB.IsOn = doorLock.isOn;
                        _context.SaveChanges();
                    } else
                    {
                        throw new Exception("Couldn't get a door lock state.");
                    }
                    return Ok(_context.DoorLocks);
                }
                else
                {
                    throw new Exception("Couldn't get a door lock state.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("set/{isOn}")]
        public async Task<IActionResult> SetAllDoorLocksStates(int isOn)
        {
            try
            {
                string url = $"{Strings.RPI_API_URL_ADRIAN}/door-locks/set/{isOn}";
                
                foreach(var doorLock in _context.DoorLocks)
                {
                    doorLock.IsOn = isOn;
                    var (response, _) = await _deviceService.SendHttpGetRequest(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Couldn't set door lock state.");
                    }
                }
                _context.SaveChanges();
                return Ok(_context.DoorLocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("set/{doorLockId}/{isOn}")]
        public async Task<IActionResult> SetOneDoorLockState(int doorLockId, int isOn)
        {
            try
            {
                string url = $"{Strings.RPI_API_URL_ADRIAN}/door-locks/set/{doorLockId}/{isOn}"; ;
                var doorLock = _context.DoorLocks.Find(doorLockId.ToString());
                if (doorLock == null)
                    throw new Exception($"Door Lock with id {doorLockId} not found in database.");

                var (response, _) = await _deviceService.SendHttpGetRequest(url);
                if (response.IsSuccessStatusCode)
                {
                    doorLock.IsOn = isOn;
                    _context.SaveChanges();
                    return Ok(_context.DoorLocks.Find(doorLockId.ToString()));
                } else
                {
                    throw new Exception("Couldn't set door lock state.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
