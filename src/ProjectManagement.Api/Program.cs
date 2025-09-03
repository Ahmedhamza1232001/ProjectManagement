using Autofac;
using Autofac.Extensions.DependencyInjection;
using ProjectManagement.Api.Extensions;
using ProjectManagement.Application;
using ProjectManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Replace default DI with Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ApplicationModule());
    containerBuilder.RegisterModule(new InfrastructureModule(builder.Configuration));
});

// Add API-specific services
builder.Services.AddApiServices();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseApiMiddlewares();

app.MapApiEndpoints();

app.Run();
