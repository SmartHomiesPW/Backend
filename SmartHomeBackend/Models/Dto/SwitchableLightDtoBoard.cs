using System.ComponentModel.DataAnnotations;

namespace SmartHomeBackend.Models.Dto
{
    public class SwitchableLightDtoBoard
    {
        public int lightId { get; set; }
        public bool isOn { get; set; }
    }
}
