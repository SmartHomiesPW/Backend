namespace SmartHomeBackend.Models.Dto
{
    public class AlarmStateDto
    {
        public string Alarm_Id { get; set; }
        public int IsActive { get; set; }
        public int IsTriggered { get; set; }
    }
}
