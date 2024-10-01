﻿using PopovaPolinaOZKT_42_21.Interfaces;
using PopovaPolinaOZKT_42_21.Interfaces.StudentsInterfaces;



namespace PopovaPolinaOZKT_42_21.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IGroupService, GroupService>();
            return services;
        }
    }
}
