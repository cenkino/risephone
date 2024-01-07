using Microsoft.Extensions.Options;
using ReportService.API.Configurations.Extensions;
using ReportService.API.Configurations.Settings;
using ReportService.API.Infrastructure.Repository;
using RisePhoneApp.Shared.Middlewares.Errors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddTransient<IReportRepository, ReportRepository>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.ConfigureRabbitMQ();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();



app.MapControllers();

app.Run();
