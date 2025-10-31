using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{


    public class ScheduleService : IScheduleService
    {


        private readonly WeekSchedule _weekSchedule;
        private int _nextId = 1;

        public ScheduleService()
        {
            _weekSchedule = new WeekSchedule(DateTime.Today);
        }

        public DaySchedule GetToday()
        {
            return _weekSchedule.Days.First(d => d.Datee.Date == DateTime.Today);
        }

        public WeekSchedule GetWeek()
        {
            return _weekSchedule;
        }

        public Schedule? GetEvent(int eventId)
        {
            return _weekSchedule.Days
                .SelectMany(d => d.Items)
                .FirstOrDefault(e => e.Id == eventId);
        }

        public void AddEvent(DateTime date, IContent content, DateTime startTime)
        {
            var day = _weekSchedule.Days.FirstOrDefault(d => d.Datee.Date == date.Date);
            if (day != null)
            {
                var schedule = new Schedule
                {
                    Id = _nextId++,
                    Content = content,
                    StartTime = startTime,
                    EndTime = startTime.AddMinutes(content.DurationInMinutes())
                };
                day.Items.Add(schedule);
            }
        }
        

        public bool RescheduleEvent(int eventId, DateTime newStartTime)
        {
            var evt = GetEvent(eventId);
            if (evt == null) return false;

            evt.StartTime = newStartTime;
            evt.EndTime = newStartTime.AddMinutes(evt.Content.DurationInMinutes());
            return true;
        }

        public bool DeleteEvent(int eventId)
        {
            foreach (var day in _weekSchedule.Days)
            {
                var evt = day.Items.FirstOrDefault(e => e.Id == eventId);
                if (evt != null)
                {
                    day.Items.Remove(evt);
                    return true;
                }
            }
            return false;
        }

        public bool AddHost(int eventId, string hostName)
        {
            var evt = GetEvent(eventId);
            if (evt?.Content is LiveStudio studio)
            {
                studio.Host = hostName; return true;
            }
            return false;
        }



        public bool AddGuest(int eventId, string guestName)
        {
            var evt = GetEvent(eventId);
            if (evt?.Content is LiveStudio studio)
            {
                studio.Guests.Add(guestName);
                return true;
            }
            return false;
        }



    }
}
