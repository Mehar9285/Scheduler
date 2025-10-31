namespace API
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Host { get; set; }
        public List<string>? Guests { get; set; } = new List<string>();
    }
}