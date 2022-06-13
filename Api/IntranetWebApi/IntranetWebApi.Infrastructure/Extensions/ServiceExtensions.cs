using IntranetWebApi.Data;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Settings;
using IntranetWebApi.Infrastructure.Data;
using IntranetWebApi.Infrastructure.Helper;
using IntranetWebApi.Infrastructure.Interfaces;
using IntranetWebApi.Models.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IntranetWebApi.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DatabaseSeeder>();

        services.AddScoped<IIntranetDbContext, IntranetDbContext>();

        var authenticationSettings = new AuthenticationSettings();

        configuration.GetSection("Authentication").Bind(authenticationSettings);

        services.AddSingleton(authenticationSettings);
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = "Bearer";
            option.DefaultScheme = "Bearer";
            option.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = authenticationSettings.JwtIssuer,
                ValidAudience = authenticationSettings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
            };

            cfg.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    c.Response.ContentType = EnumHelper.GetEnumDescription(ContentTypesEnum.TextPlain);

                    return c.Response.WriteAsync(c.Exception.ToString());
                },
                OnChallenge = c =>
                {
                    c.HandleResponse();
                    c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    c.Response.ContentType = EnumHelper.GetEnumDescription(ContentTypesEnum.ApplicationJson);

                    var result = JsonConvert.SerializeObject(new Response<string>("Nie jesteś zalogowany!"));
                    return c.Response.WriteAsync(result);
                },
                OnForbidden = c =>
                {
                    c.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    c.Response.ContentType = EnumHelper.GetEnumDescription(ContentTypesEnum.ApplicationJson);

                    var result = JsonConvert.SerializeObject(new Response<string>("Nie posiadasz dostępu do zasobu!"));
                    return c.Response.WriteAsync(result);
                }
            };
        });
    }

    public static void SeedDatabase(this IServiceScope scope)
    {
        var databaseSeeder = scope.ServiceProvider.GetService<DatabaseSeeder>();

        if (databaseSeeder is not null)
        {
            databaseSeeder.Seed();
        }
    }
}
