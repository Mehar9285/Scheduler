using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public interface IScheduleService
    {
        DaySchedule GetToday();
        WeekSchedule GetWeek();
        Schedule? GetEvent(int eventId);
        void AddEvent(DateTime date, IContent content, DateTime startTime);
        bool RescheduleEvent(int eventId, DateTime newStartTime);
        bool DeleteEvent(int eventId);
        bool AddHost(int eventId, string hostName);
        bool AddGuest(int eventId, string guestName);
        
    }
}
