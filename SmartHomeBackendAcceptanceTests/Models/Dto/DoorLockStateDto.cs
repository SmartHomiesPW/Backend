using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeBackendAcceptanceTests.Models.Dto
{
    public class DoorLockStateDto
    {
        public string doorLock_Id { get; set; }
        public string system_Id { get; set; }
        public int isOn { get; set; }
    }
}
