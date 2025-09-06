using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;
using ProjectManagement.Application.Validators.Projects;

namespace ProjectManagement.Application;

public class ApplicationModule : Autofac.Module
{
       protected override void Load(ContainerBuilder builder)
       {

              // Register FluentValidation validators via Autofac
              builder.RegisterAssemblyTypes(typeof(CreateProjectDtoValidator).Assembly)
                                .AsClosedTypesOf(typeof(IValidator<>))
                                .InstancePerLifetimeScope();

              // Register MediatR
              builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                     .AsImplementedInterfaces();
              // Register MediatR handlers manually (recommended approach for MediatR 12+)
              var assembly = typeof(ApplicationModule).Assembly;

              // Register all IRequestHandler<TRequest, TResponse>
              builder.RegisterAssemblyTypes(assembly)
                     .AsClosedTypesOf(typeof(IRequestHandler<,>))
                     .InstancePerLifetimeScope();

              // Register all IRequestHandler<TRequest> (no response)
              builder.RegisterAssemblyTypes(assembly)
                     .AsClosedTypesOf(typeof(IRequestHandler<>))
                     .InstancePerLifetimeScope();

              // Register all INotificationHandler<TNotification>
              builder.RegisterAssemblyTypes(assembly)
                     .AsClosedTypesOf(typeof(INotificationHandler<>))
                     .InstancePerLifetimeScope();

              // Register IPipelineBehavior implementations
              builder.RegisterAssemblyTypes(assembly)
                     .AsClosedTypesOf(typeof(IPipelineBehavior<,>))
                     .InstancePerLifetimeScope();

              // Register IMediator manually
              builder.RegisterType<Mediator>()
                     .As<IMediator>()
                     .InstancePerLifetimeScope();

       }

}
