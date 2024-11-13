using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PartidasContables.Constants;
using PartidasContables.DataBase.Entities;

namespace PartidasContables.DataBase
{
    public class PartidaDbSeeder
    {
        public static async Task LoadDataAsync(
            PartidaDbContext context,
            ILoggerFactory loggerFactory,
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            try
            {
                await LoadRolesAndUsersAsync(userManager, roleManager, loggerFactory);
                await LoadCuentasAsync(loggerFactory, context);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaDbContext>();
                logger.LogError(e, "Error inicializando la data del API");
            }
        }

        public static async Task LoadRolesAndUsersAsync(
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
            )
        {
            try
            {
                if (!await roleManager.Roles.AnyAsync())
                {
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.ADMIN));
                }

                if (!await userManager.Users.AnyAsync())
                {
                    var userAdmin = new UserEntity
                    {
                        FirstName = "Administrador",
                        LastName = "Blog",
                        Email = "l",
                        UserName = "admin@blogunah.edu",
                    };

                    await userManager.CreateAsync(userAdmin, "Temporal01*");

                    await userManager.AddToRoleAsync(userAdmin, RolesConstant.ADMIN);

                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaDbContext>();
                logger.LogError(e.Message);
            }
        }

        public static async Task LoadCuentasAsync(ILoggerFactory loggerFactory, PartidaDbContext _context)
        {
            try
            {
                var jsonfilePath = "SeedData/catalogocuentas.json";  // Ruta del archivo JSON con los datos de las cuentas
                var jsonContent = await File.ReadAllTextAsync(jsonfilePath);  // Leemos el archivo JSON
                var cuentas = JsonConvert.DeserializeObject<List<CatalogoCuentaEntity>>(jsonContent);  // Deserializamos el JSON en una lista de cuentas

                if (!await _context.CatalogoCuentas.AnyAsync())  // Verificamos si ya hay datos en la tabla de cuentas
                {
                    _context.CatalogoCuentas.AddRange(cuentas);  // Agregamos las cuentas a la tabla
                    await _context.SaveChangesAsync();  // Guardamos los cambios en la base de datos
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaDbContext>();
                logger.LogError(e, "Error al ejecutar el Seed de cuentas.");
            }
        }


    }
}
