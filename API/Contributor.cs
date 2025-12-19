using Microsoft.AspNetCore.Identity;

namespace API
{
    public class Contributor
    {
        public int Id { get; set; }

       
        public string IdentityUserId { get; set; } = string.Empty;
       

       
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

      
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }

        // Payment history
        public List<ScheduleEntity> Schedules { get; set; } = new();

    }
       }
