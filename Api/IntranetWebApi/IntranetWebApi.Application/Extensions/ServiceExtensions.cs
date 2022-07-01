using FluentValidation;
using FluentValidation.AspNetCore;
using IntranetWebApi.Application.Extensions;
using IntranetWebApi.Application.Features.TestowaTabelaFeatures.Command;
using IntranetWebApi.Application.Features.TestowaTabelaFeatures.Validators;
using IntranetWebApi.Application.Repository;
using IntranetWebApi.Application.Services;
using IntranetWebApi.Infrastructure.Interfaces;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IntranetWebApi.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies();

            services.AddAutoMapper(assembly);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IAccountService, AccountService>();
            services.AddSignalR();
        }
    }
}
