using CodeInTasks.Infrastructure.Persistance.EF;
using CodeInTasks.Infrastructure.Persistance.IdentityModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CodeInTasks.Infrastructure.Identity
{
    internal static class DependencyInjection
    {
        internal static void AddIdentity(this IServiceCollection services, IConfiguration config)
        {
            var jwtAuthOptions = config.GetValue<JwtAuthOptions>(ConfigConstants.JwtAuthOptionsSection);
            services.AddSingleton(_ => jwtAuthOptions);

            services.AddIdentity<User, Role>(ConfigureIdentityOptions)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization();

            services.AddAuthentication(ConfigureAuthenticationOptions)
                .AddJwtBearer(GetJwtBearerOptionsConfigurator(jwtAuthOptions));

            services.AddScoped<IJwtIdentityService, JwtIdentityService>();
        }

        private static void ConfigureIdentityOptions(IdentityOptions options)
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = DomainConstants.User_Password_MinLength;
        }

        private static void ConfigureAuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        private static Action<JwtBearerOptions> GetJwtBearerOptionsConfigurator(JwtAuthOptions jwtAuthOptions)
        {
            void Configurator(JwtBearerOptions options)
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
            }

            return Configurator;
        }
    }
}
