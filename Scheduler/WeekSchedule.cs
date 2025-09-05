using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class WeekSchedule
    {
        public List<DaySchedule> Days { get; set; } = new List<DaySchedule>();
        public WeekSchedule(DateTime startdate) {
            for (int i = 0; i < 7; i++)
            {
                Days.Add(new DaySchedule(startdate.AddDays(i)));
            }
            
        }
    }
}
