using Microsoft.Extensions.DependencyInjection;
using Bussiness.Interfaces;
using Bussiness.Services;

namespace DemoApi.Extension
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IAdminLoginService, AdminLoginService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<INotJoinStudyTimeService, NotJoinStudyTimeService>();
            services.AddTransient<IStudentCourseService, StudentCourseService>();
            services.AddTransient<IStudentLoginService, StudentLoginService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IStudentTestService, StudentTestService>();
            services.AddTransient<IStudyTimeService, StudyTimeService>();
            services.AddTransient<ITeacherLoginService, TeacherLoginService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<ITestService, TestService>();

            return services;
        }
    }
}
