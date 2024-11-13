using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                        Email = "admin@blogunah.edu",
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

    }
}
