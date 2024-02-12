using Serilog;
using Serilog.Sinks.Grafana.Loki;

using StructureLogging.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDeviceService, DeviceService>();

builder.Host.UseSerilog((ctx, conf) =>
{
    var template = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {NewLine} {Message:lj}{NewLine}{Exception}";
    conf.WriteTo.Console(outputTemplate: template);

    conf.WriteTo.File(
        "log.txt",
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
        flushToDiskInterval: TimeSpan.FromSeconds(10),
        outputTemplate: template
    );

    conf.WriteTo.Seq("http://localhost:5341");

    conf.WriteTo.GrafanaLoki(
        "http://localhost:3100",
        textFormatter: new LokiJsonTextFormatter(),
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose,
        labels: new List<LokiLabel> { new() { Key = "app", Value = "Aboba" } }
        );
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
