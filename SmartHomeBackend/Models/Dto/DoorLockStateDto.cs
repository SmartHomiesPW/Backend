using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models.Dto
{
    public class DoorLockStateDto
    {
        public string doorLock_Id { get; set; } = "Unknown";
        public string system_Id { get; set; } = "Unknown";
        public int isOn { get; set; }
    }
}
