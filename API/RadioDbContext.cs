using Microsoft.EntityFrameworkCore;

namespace API
{
    public class RadioDbContext:DbContext
    {
        public RadioDbContext(DbContextOptions<RadioDbContext> options)
            : base(options) { }
        public DbSet<ScheduleEntity> Schedules { get; set; }
    }
}
