using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackendAcceptanceTests.Models.Dto
{
    public class SwitchableLightDto
    {
        public int lightId { get; set; }
        public int isOn { get; set; }
    }
}
