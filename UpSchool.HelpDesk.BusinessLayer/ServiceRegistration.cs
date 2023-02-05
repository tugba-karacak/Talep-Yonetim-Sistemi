using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using UpSchool.HelpDesk.BusinessLayer.CQRS.AssignUser;
using UpSchool.HelpDesk.BusinessLayer.CQRS.CreateWorkRequest;
using UpSchool.HelpDesk.BusinessLayer.CQRS.CreateWorkRequestState;
using UpSchool.HelpDesk.BusinessLayer.CQRS.LoginRequest;
using UpSchool.HelpDesk.BusinessLayer.CQRS.RegisterRequest;
using UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateWorkRequest;
using UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateWorkRequestState;
using UpSchool.HelpDesk.BusinessLayer.Mappings;
using UpSchool.HelpDesk.BusinessLayer.ValidationRules;
using UpSchool.HelpDesk.DataAccessLayer;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DataAccessLayer.Concrete;

namespace UpSchool.HelpDesk.BusinessLayer
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IUow, Uow>();

            services.AddAutoMapper(conf =>
            {
                conf.AddProfiles(new List<Profile>
                {
                    new WorkRequestProfile(),
                    new ApplicationUserProfile(),
                    new WorkRequestStateProfile()
                });
            });

            services.AddFluentValidationAutoValidation();

            services.AddScoped<IValidator<AssignUserCommand>, AssignUserCommandValidator>();
            services.AddScoped<IValidator<CreateWorkRequestCommand>, CreateWorkRequestCommandValidator>();
            services.AddScoped<IValidator<CreateWorkRequestStateCommand>, CreateWorkRequestStateCommandValidator>();
            services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
            services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
            services.AddScoped<IValidator<UpdateWorkRequestCommand>, UpdateWorkRequestCommandValidator>();
            services.AddScoped<IValidator<UpdateWorkRequestStateCommand>, UpdateWorkRequestStateCommandValidator>();

            services.AddDataAccessServices(configuration);
        }
    }
}
