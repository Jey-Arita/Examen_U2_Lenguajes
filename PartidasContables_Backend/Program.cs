using Microsoft.AspNetCore.Identity;
using PartidasContables;
using PartidasContables.DataBase;
using PartidasContables.DataBase.Entities;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(app, app.Environment);
// agregar el using


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<PartidaDbContext>();
        var userManager = services.GetRequiredService<UserManager<UserEntity>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await PartidaDbSeeder.LoadDataAsync(context, loggerFactory, userManager, roleManager);

    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "Error al ejecutar el Seed de datos");
    }
}

app.Run();