using CoreWCF;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WcfService;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddServiceModelServices();
        services.AddSingleton<DataService>();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseCors("AllowAll");

        app.UseServiceModel(builder =>
        {
            builder.AddService<DataService>();
            builder.AddServiceEndpoint<DataService, IDataService>(new BasicHttpBinding(), "/DataService.svc");
        });
    }
}