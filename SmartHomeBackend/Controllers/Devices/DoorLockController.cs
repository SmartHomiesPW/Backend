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
    /// <summary>
    /// Controller responsible for managing requests associated with door locks.
    /// </summary>
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

        /// <returns>Information about current states of all door locks connected to specific board within specific system on success.</returns>
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
                    var array = JsonSerializer.Deserialize<DoorLockStateDto[]>(text) ??
                        throw new Exception("Couldn't deserialize to DoorLockStateDto[].");
                    foreach (var doorLock in array)
                    {
                        var doorLockInDB = _context.DoorLocks.Find(doorLock.doorLock_Id.ToString()) ??
                            throw new Exception($"Door Lock with id {doorLock.doorLock_Id} not found in database.");
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

        /// <returns>Information about current states of all door locks connected to specific board within specific system on success.</returns>
        [Route("states/{doorLockId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneDoorLockState(int doorLockId)
        {
            try
            {
                string url = $"{Strings.RPI_API_URL_ADRIAN}/door-locks/states/{doorLockId}";
                var doorLockInDB = _context.DoorLocks.Find(doorLockId.ToString()) ??
                    throw new Exception($"Door Lock with id {doorLockId} not found in database.");

                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var doorLock = JsonSerializer.Deserialize<DoorLockStateDto>(text) ??
                        throw new Exception("Couldn't deserialize to DoorLockStateDto[].");

                    doorLockInDB.IsOn = doorLock.isOn;
                    _context.SaveChanges();

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
        /// <summary>Sets state of all door locks connected to specific board within specific system</summary>
        /// <returns>All door locks states in database on success.</returns>
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

        /// <summary>Sets state of a specific door lock connected to specific board within specific system</summary>
        /// <returns>A specific door lock state in database on success.</returns>
        [HttpPut]
        [Route("set/{doorLockId}/{isOn}")]
        public async Task<IActionResult> SetOneDoorLockState(int doorLockId, int isOn)
        {
            try
            {
                string url = $"{Strings.RPI_API_URL_ADRIAN}/door-locks/set/{doorLockId}/{isOn}";
                var doorLock = _context.DoorLocks.Find(doorLockId.ToString()) ??
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
