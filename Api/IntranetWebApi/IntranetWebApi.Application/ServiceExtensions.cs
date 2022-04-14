using FluentValidation;
using IntranetWebApi.Application.Repository;
using IntranetWebApi.Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
