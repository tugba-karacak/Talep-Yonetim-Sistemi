using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.DataAccessLayer.Contexts;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.DataAccessLayer
{
    public static class ServiceRegistration
    {
        public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HelpDeskContext>(conf =>
            {
                conf.UseSqlServer(configuration.GetConnectionString("LocalDatabase"));
            });


        }
    }
}
