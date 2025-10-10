using API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RadioDbContext>(options => options
.UseSqlite(builder.Configuration.GetConnectionString(
    "DefaultConnection")));
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped <IScheduleService , ScheduleService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Radio Station Scheduler");

             // Get today's schedule
app.MapGet("/schedule/today", async ([FromServices] IScheduleService scheduler,
                                     [FromServices] RadioDbContext db) =>
{
    var today = scheduler.GetToday();
    today.AutoFillTheMusic();

    foreach (var item in today.Items)
    {
        if (!await db.Schedules.AnyAsync(s => s.Title == item.Content.Title() && s.StartTime == item.StartTime))
        {
            db.Schedules.Add(new ScheduleEntity
            {
                Title = item.Content.Title(),
                ContentType = item.Content.GetType().Name,
                Date = item.StartTime.Date,
                StartTime = item.StartTime,
                EndTime = item.EndTime
            });
        }
    }

    await db.SaveChangesAsync();
    return Results.Ok(today.Items);
});

                // Get weekly schedule
app.MapGet("/schedule/week", ([FromServices] IScheduleService scheduler) =>
{
    var week = scheduler.GetWeek();
    return Results.Ok(week.Days);
});

            // Get event by ID
app.MapGet("/schedule/{id:int}", ([FromServices] IScheduleService scheduler, int id) =>
{
    var evt = scheduler.GetEvent(id);
    return evt is null ? Results.NotFound() : Results.Ok(evt);
});

               // Add a new event
app.MapPost("/schedule/add", async ([FromServices] IScheduleService scheduler,
                                    [FromServices] RadioDbContext db,
                                    DateTime date, string contentType, string name, DateTime startTime) =>
{
    IContent content = contentType.ToLower() switch
    {
        "music" => new Music(name, 30, true),
        "prerecording" => new PreRecording(name, 45, true),
        "studio" => new LiveStudio(name, 60, true),
        _ => throw new ArgumentException("Invalid content type")
    };

    scheduler.AddEvent(date, content, startTime);

    db.Schedules.Add(new ScheduleEntity
    {
        Title = name,
        ContentType = contentType,
        Date = date.Date,
        StartTime = startTime,
        EndTime = startTime.AddMinutes(content.DurationInMinutes())
    });

    await db.SaveChangesAsync();
    return Results.Ok("Event added");
});

           // Reschedule an event
app.MapPut("/schedule/{id:int}/reschedule", ([FromServices] IScheduleService scheduler, int id, DateTime newStartTime) =>
{
    return scheduler.RescheduleEvent(id, newStartTime)
        ? Results.Ok("Rescheduled")
        : Results.NotFound();
});

           // Delete an event
app.MapDelete("/schedule/{id:int}", ([FromServices] IScheduleService scheduler, int id) =>
{
    return scheduler.DeleteEvent(id)
        ? Results.Ok("Deleted")
        : Results.NotFound();
});

              // Add host to a studio event
app.MapPost("/schedule/{id:int}/host", ([FromServices] IScheduleService scheduler, int id, string hostName) =>
{
    return scheduler.AddHost(id, hostName)
        ? Results.Ok("Host added")
        : Results.BadRequest("Event must be a studio type");
});

             // Add guest to a studio event
app.MapPost("/schedule/{id:int}/guest", ([FromServices] IScheduleService scheduler, int id, string guestName) =>
{
    return scheduler.AddGuest(id, guestName)
        ? Results.Ok("Guest added")
        : Results.BadRequest("Event must be a studio type");
});


app.Run();
