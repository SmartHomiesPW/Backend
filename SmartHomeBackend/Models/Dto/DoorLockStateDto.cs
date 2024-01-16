using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackend.Models.Dto
{
    public class DoorLockStateDto
    {
        public string doorLock_Id { get; set; }
        public string system_Id { get; set; }
        public int isOn { get; set; }
    }
}
