using CodeInTasks.Infrastructure.EF;
using CodeInTasks.Infrastructure.Identity;
using CodeInTasks.Infrastructure.Queues;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CodeInTasks.Infrastructure
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddEfPersistance(config);
            services.AddIdentity(config);
            services.AddQueues();

            return services;
        }

        private static void AddEfPersistance(
            this IServiceCollection services,
            IConfiguration config)
        {
            var dbOptions = config.GetValue<EfDbOptions>(ConfigSections.EfDbOptions);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    dbOptions.ConnectionString,
                    options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });
            services.AddScoped(typeof(IRepository<>), typeof(EfGenericRepository<>));
        }

        private static void AddIdentity(
            this IServiceCollection services,
            IConfiguration config)
        {
            var jwtAuthOptions = config.GetValue<JwtAuthOptions>(ConfigSections.JwtAuthOptions);

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = jwtAuthOptions.Audience,
                    ValidIssuer = jwtAuthOptions.Issuer,
                    IssuerSigningKey = jwtAuthOptions.SymmetricSecurityKey
                };
            });

            services.AddScoped<IJwtIdentityService, JwtIdentityService>();
        }

        private static void AddQueues(this IServiceCollection services)
        {
            services.AddScoped<ISolutionQueue, SolutionQueue>();
        }
    }
}
