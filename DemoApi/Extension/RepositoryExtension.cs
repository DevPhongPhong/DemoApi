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
    public static class RepositoryExtension
    {
        public static IServiceCollection AddCustomRepository(this IServiceCollection services)
        {
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IAdminLoginRepository, AdminLoginRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<INotJoinStudyTimeRepository, NotJoinStudyTimeRepository>();
            services.AddTransient<IStudentCourseRepository, StudentCourseRepository>();
            services.AddTransient<IStudentLoginRepository, StudentLoginRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentTestRepository, StudentTestRepository>();
            services.AddTransient<IStudyTimeRepository, StudyTimeRepository>();
            services.AddTransient<ITeacherLoginRepository, TeacherLoginRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<ITestRepository, TestRepository>();

            return services;
        }
    }
}
