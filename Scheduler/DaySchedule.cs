using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Scheduler
{
    public class DaySchedule
    {
        public List<Schedule> Items { get; set; } = new List<Schedule>(); //list of all schedule items
        public DateTime Datee { get; set; } // date of this day
        public DaySchedule(DateTime date) //constructor to pass a date when a new schedule is created
        {
            Datee = date;

        }
        // Add Content and its specific time to the schedule
        public void Add(IContent content, DateTime startTime)
        {
            var item = new Schedule();
            item.Content = content; // The content
            item.StartTime = startTime; // The start time
            item.EndTime = startTime.AddMinutes(content.DurationInMinutes());// Take the length of the program and add that as minutes to the start time to get the end time
            Items.Add(item); // Add  new items to the list
        }
        // No gap exists by filling music
        public void AutoFillTheMusic()
        {
            Items = Items.OrderBy(content => content.StartTime).ToList(); // Ensue that the items in the list are in order
            DateTime current = Datee.Date;
            DateTime endOfDay = current.AddDays(1);
            List<Schedule> newitems = new List<Schedule>(); // New list to store gap free schedule
            foreach (var item in Items)
            {
                if (item.StartTime > current)
                {
                    newitems.Add(new Schedule { Content = new Music("Auto Music", ((int)(item.StartTime - current).TotalMinutes), true),
                        StartTime = current,
                        EndTime = item.StartTime });

                }
                newitems.Add(item);
                current = item.EndTime;
            }

            if (current < endOfDay)
            {
                newitems.Add(new Schedule { Content = new Music("Auto Music", ((int)(endOfDay - current).TotalMinutes), true),
                    StartTime = current,
                    EndTime = endOfDay });
            }
            Items = newitems;
        }
    }
}
                

            
    

