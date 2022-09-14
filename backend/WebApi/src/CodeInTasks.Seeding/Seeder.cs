using CodeInTasks.Application.Abstractions;
using CodeInTasks.Application.Abstractions.Exceptions;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure;
using CodeInTasks.Infrastructure.Persistance.EF;
using CodeInTasks.Infrastructure.Persistance.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeInTasks.Seeding
{
    public class Seeder
    {
        private readonly SeedingOptions seedingOptions;
        private readonly AppDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IIdentityService identityService;
        private readonly ILogger<Seeder> logger;

        public Seeder(
            SeedingOptions seedingOptions,
            AppDbContext dbContext,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IIdentityService identityService,
            ILogger<Seeder> logger)
        {
            this.seedingOptions = seedingOptions;
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.identityService = identityService;
            this.logger = logger;
        }

        public async Task SeedAsync()
        {
            await MigrateAsync();

            await SeedRoles();

            await SeedAdminAccountAsync();

            await SeedBuilderAccountAsync();

            await dbContext.SaveChangesAsync();
        }

        private async Task MigrateAsync()
        {
            logger.LogInformation("Applying migrations");

            await dbContext.Database.MigrateAsync();

            logger.LogInformation("Migrated successfully");
        }

        private async Task SeedRoles()
        {
            logger.LogInformation("Seeding roles");

            var roles = new[] { RoleNames.Creator, RoleNames.Manager, RoleNames.Admin, RoleNames.Builder };

            foreach (var role in roles)
            {
                await SeedRoleAsync(role);
            }

            logger.LogInformation("Role seeding finished");
        }

        private async Task SeedRoleAsync(string roleName)
        {
            logger.LogInformation("Trying to seed role \"{roleName}\"", roleName);

            var isRoleExists = await roleManager.RoleExistsAsync(roleName);

            if (isRoleExists)
            {
                logger.LogInformation("Role \"{roleName}\" already exists", roleName);

                return;
            }

            await CreateRoleAsync(roleName);
        }

        private async Task CreateRoleAsync(string roleName)
        {
            logger.LogInformation("Role \"{roleName}\" not exists => creating", roleName);

            var role = new Role()
            {
                Name = roleName,
            };

            var result = await roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                logger.LogInformation("Successfully created role \"{roleName}\"", roleName);
            }
            else
            {
                var errors = string.Join(',', result.Errors.Select(x => x.Code));

                logger.LogError("Error creating role \"{roleName}\"! Errors: {errors}", roleName, errors);
            }
        }

        private async Task SeedAdminAccountAsync()
        {
            logger.LogInformation("Seeding admin account");

            var admins = await userManager.GetUsersInRoleAsync(RoleNames.Admin);

            if (admins.Any())
            {
                logger.LogWarning("Admin already exists. Skipping");

                return;
            }

            await CreateAdminAsync();
        }

        private async Task CreateAdminAsync()
        {
            logger.LogInformation("Creating admin account");

            var email = seedingOptions.AdminEmail;
            var password = seedingOptions.AdminPassword;
            const string name = "Admin";

            try
            {
                await identityService.CreateUserAsync(new() { Email = email, Password = password, Name = name });

                var user = await userManager.FindByEmailAsync(email);

                var result = await userManager.AddToRoleAsync(user, RoleNames.Admin);

                if (result.Succeeded)
                {
                    logger.LogInformation("Admin seeded successfully");
                }
                else
                {
                    throw new IdentityException(result.Errors.Select(x => x.Code));
                }

            }
            catch (Exception exception)
            {
                logger.LogCritical(exception, "Admin account not seeded, no need to start without it");
                throw;
            }
        }

        private async Task SeedBuilderAccountAsync()
        {
            logger.LogInformation("Seeding builder account");

            var builders = await userManager.GetUsersInRoleAsync(RoleNames.Builder);

            if (builders.Any())
            {
                logger.LogWarning("Builder already exists. Skipping");

                return;
            }

            await CreateBuilderAsync();
        }

        private async Task CreateBuilderAsync()
        {
            logger.LogInformation("Creating builder account");

            var email = seedingOptions.BuilderEmail;
            var password = seedingOptions.BuilderPassword;
            const string name = "Builder";

            try
            {
                await identityService.CreateUserAsync(new() { Email = email, Password = password, Name = name });

                var user = await userManager.FindByEmailAsync(email);

                var result = await userManager.AddToRoleAsync(user, RoleNames.Builder);

                if (result.Succeeded)
                {
                    logger.LogInformation("Builder seeded successfully");
                }
                else
                {
                    throw new IdentityException(result.Errors.Select(x => x.Code));
                }

            }
            catch (Exception exception)
            {
                logger.LogCritical(exception, "Builder account not seeded, no need to start without it");
                throw;
            }
        }
    }
}
