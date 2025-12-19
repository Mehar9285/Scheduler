using System.Text.Json.Serialization;

namespace API
{
    public class ScheduleEntity
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty; 
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Host { get; set; }
        public string? Guests { get; set; }
       public int? ContributorId { get; set; }
        [JsonIgnore]
        public Contributor? Contributor { get; set; }

    }
}
