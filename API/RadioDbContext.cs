using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class RadioDbContext:IdentityDbContext<IdentityUser>
    {
        public RadioDbContext(DbContextOptions<RadioDbContext> options)
            : base(options) { }
        public DbSet<ScheduleEntity> Schedules { get; set; }
        public DbSet<Contributor> Contributors { get; set; }

       
    }
}
