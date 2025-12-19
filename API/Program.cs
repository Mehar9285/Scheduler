using API;
using API.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IScheduleService, ScheduleService>();






builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<RadioDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;

    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
    
}
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RadioDbContext>(options =>
    options.UseSqlite("Data Source=radio.db"));
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RadioDbContext>();
    var scheduler = scope.ServiceProvider.GetRequiredService<IScheduleService>();

    foreach (var s in db.Schedules.ToList())
    {
        IContent content = s.ContentType.ToLower() switch
        {
            "music" => new Music(s.Title, 30, true),
            "prerecording" => new PreRecording(s.Title, 45, true),
            "studio" => new LiveStudio(s.Title, 60, true),
            _ => new Music(s.Title, 30, true)
        };




        scheduler.AddEvent(s.Date, content, s.StartTime);
    }
}
app.UseCors("AllowReactApp");
app.UseAuthentication(); 
app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();

app.MapPost("/logout", async (SignInManager<IdentityUser> signInManager)=>

{
    await signInManager.SignOutAsync();
    return Results.Ok();
})
.RequireAuthorization()
.WithOpenApi();


app.MapGet("/", () => "Radio Station Scheduler");

app.MapGet("/schedule/today", async ([FromServices] IScheduleService scheduler,
                                     [FromServices] RadioDbContext db) =>
{
    var today = scheduler.GetToday();
    today.AutoFillTheMusic();

    var todayDate = DateTime.Today;

    
    foreach (var item in today.Items)
    {
        if (!await db.Schedules.AnyAsync(s => s.Title == item.Content.Title() &&
                s.StartTime == item.StartTime))
        {
            db.Schedules.Add(new ScheduleEntity
            {
                Title = item.Content.Title(),
                ContentType = item.Content.GetType().Name.ToLower(),
                Date = item.StartTime.Date,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                Host = item.Content is LiveStudio studio ? studio.Host : null,
                Guests = item.Content is LiveStudio studio2 && studio2.Guests != null
                    ? string.Join(", ", studio2.Guests)
                    : null
            });
        }
    }

    await db.SaveChangesAsync();

    
    var existingEvents = await db.Schedules
        .Where(s => s.Date == todayDate)
        .OrderBy(s => s.StartTime)
        .ToListAsync();

    
    var result = existingEvents.Select(s => new ScheduleDto
    {
        Id = s.Id,
        Title = s.Title,
        ContentType = s.ContentType,
        StartTime = s.StartTime,
        EndTime = s.EndTime,
        Host = s.Host,
        Guests = !string.IsNullOrEmpty(s.Guests)
            ? s.Guests.Split(',').Select(g => g.Trim()).ToList()
            : new List<string>()
    }).ToList();

    return Results.Ok(result);
});


app.MapGet("/schedule/week", async ([FromServices] RadioDbContext db) =>
{
    var today = DateTime.Today;
    var endOfWeek = today.AddDays(7);

    var events = await db.Schedules
        .Where(s => s.Date >= today && s.Date <= endOfWeek)
        .OrderBy(s => s.Date)
        .ThenBy(s => s.StartTime)
        .ToListAsync();

    var result = events
        .GroupBy(s => s.Date)
        .Select(g => new
        {
            date = g.Key,
            items = g.Select(s => new ScheduleDto
            {
                Id = s.Id,
                Title = s.Title,
                ContentType = s.ContentType,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Host = s.Host,
                Guests = !string.IsNullOrEmpty(s.Guests)
                    ? s.Guests.Split(',').Select(g => g.Trim()).ToList()
                    : new List<string>()
            }).ToList()
        })
        .ToList();

    return Results.Ok(result);
});


app.MapGet("/schedule/{id:int}", ([FromServices] IScheduleService scheduler, int id) =>
{
    var evt = scheduler.GetEvent(id);
    if (evt == null) return Results.NotFound();

    var dto = new ScheduleDto
    {
        Id = evt.Id,
        Title = evt.Content.Title(),
        ContentType = evt.Content.GetType().Name.ToLower(),
        StartTime = evt.StartTime,
        EndTime = evt.EndTime,
        Host = evt.Content is LiveStudio studio ? studio.Host : null,
        Guests = evt.Content is LiveStudio studio2 ? studio2.Guests : null
    };

    return Results.Ok(dto);
});

app.MapPost("/schedule/add",
    async (
        [FromServices] IScheduleService scheduler,
        [FromServices] RadioDbContext db,
        HttpContext http,
        [FromBody] AddEventRequest request
    ) =>
    {
        var userId = http.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Results.Unauthorized();

        var contributor = await db.Contributors
            .FirstOrDefaultAsync(c => c.IdentityUserId == userId);

        if (contributor == null)
            return Results.BadRequest("Contributor not registered");

        IContent content = request.ContentType.ToLower() switch
        {
            "music" => new Music(request.Name, 30, true),
            "prerecording" => new PreRecording(request.Name, 45, true),
            "studio" => new LiveStudio(request.Name, 60, true),
            _ => throw new ArgumentException("Invalid content type")
        };

        scheduler.AddEvent(request.Date, content, request.StartTime);

        db.Schedules.Add(new ScheduleEntity
        {
            Title = request.Name,
            ContentType = request.ContentType.ToLower(),
            Date = request.Date.Date,
            StartTime = request.StartTime,
            EndTime = request.StartTime.AddMinutes(content.DurationInMinutes()),
            ContributorId = contributor.Id,
            Host = contributor.FullName
            });

        await db.SaveChangesAsync();
        return Results.Ok("Event added");
    })
.RequireAuthorization();

app.MapPut("/schedule/{id:int}/reschedule", async ([FromServices] RadioDbContext db,
                                                   int id,
                                                   [FromBody] RescheduleRequest request) =>
{
    var evt = await db.Schedules.FindAsync(id);
    if (evt == null) return Results.NotFound();

    evt.StartTime = request.NewStartTime;
    evt.EndTime = request.NewStartTime.AddMinutes(
        evt.ContentType.ToLower() == "music" ? 30 :
        evt.ContentType.ToLower() == "prerecording" ? 45 : 60
    );

    await db.SaveChangesAsync();
    return Results.Ok("Rescheduled");
});

app.MapDelete("/schedule/{id:int}", async ([FromServices] RadioDbContext db, int id) =>
{
    var evt = await db.Schedules.FindAsync(id);
    if (evt == null) return Results.NotFound();

    db.Schedules.Remove(evt);
    await db.SaveChangesAsync();
    return Results.Ok("Deleted");
});

app.MapPost("/schedule/{id:int}/host",
    async ([FromServices] RadioDbContext db,
           int id,
           [FromBody] HostGuestRequest request,
           HttpContext http) =>
    {
        var evt = await db.Schedules.FindAsync(id);
        if (evt == null) return Results.NotFound();
         var userId = http.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var contributor = await db.Contributors.FirstOrDefaultAsync(c => c.IdentityUserId == userId);

        if (contributor == null)
            return Results.BadRequest("Contributor not registered");

        evt.Host = contributor.FullName;
        evt.ContributorId = contributor.Id;

        await db.SaveChangesAsync();
        return Results.Ok("Host added");
    });

app.MapPost("/schedule/{id:int}/guest", async ([FromServices] RadioDbContext db,
                                               int id,
                                               [FromBody] HostGuestRequest request) =>
{
    var evt = await db.Schedules.FindAsync(id);
    if (evt == null) return Results.NotFound();

    if (evt.ContentType.ToLower() != "studio")
        return Results.BadRequest("Event must be a studio type");

    var guests = string.IsNullOrEmpty(evt.Guests)
        ? request.Name
        : evt.Guests + ", " + request.Name;

    evt.Guests = guests;
    await db.SaveChangesAsync();
    return Results.Ok("Guest added");
});

app.MapPost("/contributors/create",
    async ([FromServices] RadioDbContext db,
           HttpContext http,
           [FromBody] Contributor request) =>
          {
        var userId = http.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Results.Unauthorized();
        request.IdentityUserId = userId;
           db.Contributors.Add(request);
        await db.SaveChangesAsync();
        return Results.Ok(request);
       })
      .RequireAuthorization();



app.MapGet("/contributors/me",
    async (RadioDbContext db, HttpContext http) =>
    {
        var userId = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Results.Unauthorized();

        var contributor = await db.Contributors
            .Include(c => c.Schedules)
            .FirstOrDefaultAsync(c => c.IdentityUserId == userId);

        if (contributor == null)
            return Results.NotFound();

        return Results.Ok(contributor);
    })
.RequireAuthorization();

app.MapGet("/contributors/{id:int}/calculate",
    async (int id, RadioDbContext db) =>
    {
        var events = await db.Schedules
            .Where(s => s.ContributorId == id)
            .ToListAsync();

        var totalHours = events.Sum(e =>
            (decimal)(e.EndTime - e.StartTime).TotalHours);

        var numberOfEvents = events.Count;

        decimal totalAmount =
            totalHours * 750m +
            numberOfEvents * 300m;

        decimal totalAmountWithVat = totalAmount * 1.25m;

        return Results.Ok(new
        {
            totalHours,
            numberOfEvents,
            totalAmount,
            totalAmountWithVat,
            month = DateTime.Now.ToString("MMMM")
        });
    })
.RequireAuthorization();

app.Run();























