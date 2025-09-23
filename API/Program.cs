using Scheduler;

var builder = WebApplication.CreateBuilder(args);
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
app.MapGet("/schedule/today", (IScheduleService scheduler) =>
{
    var today = scheduler.GetToday();
    today.AutoFillTheMusic();
    return Results.Ok(today.Items); // Return the items for today
});

app.MapGet("/schedule/week", (IScheduleService scheduler) =>
{
    return Results.Ok(scheduler.GetWeek().Days); // Return the full week's days
});

app.MapGet("/schedule/{id:int}", (IScheduleService scheduler, int id) =>
{
    var evt = scheduler.GetEvent(id);
    return evt is null ? Results.NotFound() : Results.Ok(evt);
});

app.MapPost("/schedule/add", (IScheduleService scheduler, DateTime date, string contentType, string name, DateTime startTime) =>
{
    IContent content = contentType.ToLower() switch
    {
        "music" => new Music(name, 30, true),
        "prerecording" => new PreRecording(name, 45, true),
        "studio" => new LiveStudio(name, 60, true),
        _ => throw new ArgumentException("Invalid content type")
    };

    scheduler.AddEvent(date, content, startTime);
    return Results.Ok("Event added");
});

app.MapPut("/schedule/{id:int}/reschedule", (IScheduleService scheduler, int id, DateTime newStartTime) =>
{
    return scheduler.RescheduleEvent(id, newStartTime)
        ? Results.Ok("Rescheduled")
        : Results.NotFound();
});

app.MapDelete("/schedule/{id:int}", (IScheduleService scheduler, int id) =>
{
    return scheduler.DeleteEvent(id)
        ? Results.Ok("Deleted")
        : Results.NotFound();
});

app.MapPost("/schedule/{id:int}/host", (IScheduleService scheduler, int id, string hostName) =>
{
    return scheduler.AddHost(id, hostName)
        ? Results.Ok("Host added")
        : Results.BadRequest("Event must be a studio type");
});

app.MapPost("/schedule/{id:int}/guest", (IScheduleService scheduler, int id, string guestName) =>
{
    return scheduler.AddGuest(id, guestName)
        ? Results.Ok("Guest added")
        : Results.BadRequest("Event must be a studio type");
});

app.Run();
