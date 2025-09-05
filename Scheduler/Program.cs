// See https://aka.ms/new-console-template for more information
using Scheduler;

WeekSchedule weekSchedule = new WeekSchedule(DateTime.Today);


foreach (var day in weekSchedule.Days)
{
    Console.WriteLine("Schedule for " + day.Datee.ToString("dddd, MMMM dd"));

    foreach (var item in day.Items)
    {
        Console.WriteLine(item.Content.Title() +
                           " : " + item.StartTime.ToString("HH:mm") +
                           " - " + item.EndTime.ToString("HH:mm"));
    }

   
}

