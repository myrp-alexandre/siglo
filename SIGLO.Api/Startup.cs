using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIGLO.Domain.Handlers;
using SIGLO.Domain.Repositories;
using SIGLO.Infra.Context;
using SIGLO.Infra.Repositories;
using SIGLO.Infra.Transations;
using SIGLO.Shared;
using System.IO;

namespace SIGLO.Api
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            AppSettings.ConnectionString = $"{Configuration["connectionString"]}";

            services.AddMvc();
            services.AddCors();

            services.Configure<IISOptions>(o =>
            {
                o.ForwardClientCertificate = false;
            });

            services.AddResponseCompression();

            services.AddSingleton(SIGLODataContext_NH.SessionFactory());
            services.AddScoped<SIGLODataContext_DP, SIGLODataContext_DP>();

            services.AddTransient<IUow, Uow>();

            services.AddTransient<IContractRepository, ContractRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICallTypeRepository, CallTypeRepository>();
            services.AddTransient<ICallRepository, CallRepository>();
            services.AddTransient<ICallGroupRepository, CallGroupRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ISectorRepository, SectorRepository>();

            services.AddTransient<ContractHandler, ContractHandler>();
            services.AddTransient<AccountHandler, AccountHandler>();
            services.AddTransient<CallTypeHandler, CallTypeHandler>();
            services.AddTransient<CallGroupHandler, CallGroupHandler>();
            services.AddTransient<CallHandler, CallHandler>();
            services.AddTransient<AccountHandler, AccountHandler>();
            services.AddTransient<SectorHandler, SectorHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

            app.UseCors(x => {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseResponseCompression();

            app.UseMvc();
        }
    }
}
