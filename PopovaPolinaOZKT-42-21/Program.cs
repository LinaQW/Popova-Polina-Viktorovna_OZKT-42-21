using PopovaPolinaOZKT_42_21.DataBase;
//using PopovaPolinaOZKT_42_21.Middlewares;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Diagnostics;
//using static PopovaPolinaOZKT_42_21.ServiceExtensions.ServiceExtensions;


var builder = WebApplication.CreateBuilder(args);

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<StudentDbContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    /* var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
     builder.Services.AddDbContext<student_db>(options =>
     options.UseSqlServer(connectionString));*/

    // builder.Services.AddServices();
    // AddCustomServices(builder.Services);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
}
finally
{
    LogManager.Shutdown();
}