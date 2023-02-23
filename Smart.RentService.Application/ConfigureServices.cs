using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Reflection;
using FluentValidation;
using Smart.RentService.Application.ContractAggregate.Behaviours;

namespace Smart.RentService.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddMediatR(o =>
                {
                    o.RegisterServicesFromAssemblyContaining(typeof(ValidationBehaviour<,>));
                })
                .AddValidatorsFromAssemblyContaining(typeof(ValidationBehaviour<,>))
                .AddBehaviours();
        }

        private static IServiceCollection AddBehaviours(this IServiceCollection services)
        {
            return
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        }
    }
}
