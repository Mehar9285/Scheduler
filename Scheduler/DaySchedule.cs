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
        public List<Schedule> Items { get; set; } = new List<Schedule>(); 
        public DateTime Datee { get; set; } 
        public DaySchedule(DateTime date) 
        {
            Datee = date;

        }
        
        public void Add(IContent content, DateTime startTime)
        {
            var item = new Schedule();
            item.Content = content;
            item.StartTime = startTime; 
            item.EndTime = startTime.AddMinutes(content.DurationInMinutes());// Take the length of the program and add that as minutes to the start time to get the end time
            Items.Add(item); 
        }
        
        public void AutoFillTheMusic()
        {
            Items = Items.OrderBy(content => content.StartTime).ToList(); 
            DateTime current = Datee.Date;
            DateTime endOfDay = current.AddDays(1);
            List<Schedule> newitems = new List<Schedule>(); 
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
                

            
    

