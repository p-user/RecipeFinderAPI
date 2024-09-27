using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplicationServices( this IServiceCollection services)
        {

                services.AddAutoMapper(Assembly.GetExecutingAssembly());
                services.AddMediatR(cf => cf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
                services.AddScoped<IUserContext, UserContext>();


        }
    }
}
