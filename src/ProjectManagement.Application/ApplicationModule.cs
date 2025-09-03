using Autofac;
using ProjectManagement.Application.Abstractions.Services;
using ProjectManagement.Application.Services;

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
    }
}
