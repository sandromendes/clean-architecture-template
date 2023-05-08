using Application.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class RegisterService
    {
        public static void ConfigureApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(_ => _.RegisterServicesFromAssembly(typeof(IApplicationDbContext).Assembly));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
