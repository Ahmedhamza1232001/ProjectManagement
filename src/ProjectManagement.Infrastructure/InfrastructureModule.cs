using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectManagement.Application.Abstractions.Services;
using ProjectManagement.Infrastructure.Persistence;
using ProjectManagement.Infrastructure.Services;

namespace ProjectManagement.Infrastructure;

public class InfrastructureModule : Module
{
    private readonly IConfiguration _configuration;

    public InfrastructureModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        // Register DbContext
        builder.Register(context =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectManagementDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            return new ProjectManagementDbContext(optionsBuilder.Options);
        }).AsSelf().InstancePerLifetimeScope();

        // Register repositories if you have them
        // Register infrastructure-level services
        builder.RegisterType<AuthService>()
               .As<IAuthService>()
               .InstancePerLifetimeScope();

        builder.RegisterType<EmailService>()
               .As<IEmailService>()
               .InstancePerLifetimeScope();

        builder.RegisterType<NotificationService>()
               .As<INotificationService>()
               .InstancePerLifetimeScope();
        // builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
    }
}
