using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackend.Models.Dto
{
    public class SwitchableLightDto
    {
        public int lightId { get; set; }
        public bool isOn { get; set; }
    }
}
