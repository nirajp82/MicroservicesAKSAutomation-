using Microsoft.Extensions.DependencyInjection;
using Shopping.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Infrastructure
{
    public static class ServiceExtensions
    {
        #region Extension Method
        public static void ConfigureInfrastructureServices(this IServiceCollection services)
        {
            //Services Registration
            services.AddScoped<IProductRepository, ProductRepository>();
        }
        #endregion        
    }
}
