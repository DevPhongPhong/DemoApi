using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bussiness.Interfaces;
using Bussiness.Services;
using Repository.Interfaces;
using Repository.Repositories;

namespace DemoApi.Extension
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IStudentJoinCourseRepository, StudentJoinCourseRepository>();
            return services;
        }
    }
}
