using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackend.Models.Dto
{
    public class SwitchableLightDto
    {
        public int LightId { get; set; }
        public bool IsOn { get; set; }
    }
}
