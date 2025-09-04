using Autofac;
using FluentValidation;
using ProjectManagement.Application.Abstractions.Services;
using ProjectManagement.Application.Services;
using ProjectManagement.Application.Validators.Projects;

namespace ProjectManagement.Application;

public class ApplicationModule : Module
{
       protected override void Load(ContainerBuilder builder)
       {
              // Register application services
              builder.RegisterType<UserService>()
                     .As<IUserService>()
                     .InstancePerLifetimeScope();

              builder.RegisterType<ProjectService>()
                     .As<IProjectService>()
                     .InstancePerLifetimeScope();

              builder.RegisterType<TaskService>()
                     .As<ITaskService>()
                     .InstancePerLifetimeScope();

              builder.RegisterAssemblyTypes(typeof(CreateProjectDtoValidator).Assembly)
                                .AsClosedTypesOf(typeof(IValidator<>))
                                .InstancePerLifetimeScope();
       }
}
