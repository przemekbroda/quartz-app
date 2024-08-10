using Quartz;
using QuartzApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("testJob");

    q.AddJob<TestJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("testJob-trigger")
        .WithCronSchedule("0/5 * * * * ?")
    );
});
builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
builder.Services.Configure<QuartzOptions>(builder.Configuration.GetSection("Quartz"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/hello-world", () => Results.Ok("Hello World"));

app.Run();