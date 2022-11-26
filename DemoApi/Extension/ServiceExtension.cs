﻿using Microsoft.Extensions.DependencyInjection;
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
            //service
            services.AddTransient<ICourseService, CourseService>(); 
            services.AddTransient<INotJoinStudyTimeService, NotJoinStudyTimeService>(); 
            services.AddTransient<IStudentCourseService, StudentCourseService>();
            services.AddTransient<IStudentLoginService, StudentLoginService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IStudentTestService, StudentTestService>();
            services.AddTransient<IStudyTimeService, StudyTimeService>();
            services.AddTransient<ITeacherLoginService,TeacherLoginService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<ITestService, TestService>();
            //repository
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
