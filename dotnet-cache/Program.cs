using dotnet_cache.Infra.Config;
using dotnet_cache.Infra;
using dotnetCache.Service;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [DB]
string postgresConnectionString = builder.Configuration.GetSection("Connections:PostgreSQL").Value;

builder.Services.AddHealthChecks()
    .AddNpgSql(
        postgresConnectionString,
        name: "PostgreSQL",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "db", "sql", "postgres" });
#endregion


#region [DI]
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("Connections"));
builder.Services.AddSingleton<DbConnectionProvider>();
builder.Services.AddScoped<TimeSeriesRepository>();
builder.Services.AddTransient<ITimeSeriesService, TimeSeriesService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region [Healthcheck]
app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,

});

#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
