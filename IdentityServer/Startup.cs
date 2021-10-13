using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using IdentityServer4.AspNetIdentity;
using QueryService;
using Domain.Identity.Entities;
using IdentityServer.Application;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                // .AddScoped<UserManager<User>, UserIdentityManager>()
                .AddIdentity<User, Role>()
                .AddUserStore<UserIdentityService>()
                .AddRoleStore<RoleService>()
                .Services.AddIdentityServer(options => 
                {
                    options.IssuerUri = "http://localhost:5000";
                    options.LowerCaseIssuerUri = true;

                }).AddDeveloperSigningCredential()
                    .AddAspNetIdentity<User>()
                    .AddClientStore<ClientIdentityInfoQuery>()
                    .AddResourceStore<ResourceQuery>();

            // services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User>>();
            // services.AddScoped<UserManager<User>>();
            // services.AddOptions<IdentityOptions>().Configure(options => 
            // {
            //     options.User.RequireUniqueEmail = false;
            // });
            
            // services.AddScoped<UserIdentityService>();
            // services.AddIdentity<User, Role>().AddUserStore<UserIdentityService>();
            // services.AddTransient<UserManager<User>>();
            // services.AddTransient<IUserClaimsPrincipalFactory<User>>(sp => sp.GetRequiredService<UserIdentityService>());
            // services.AddIdentityServer(options => 
            // {
            //     options.IssuerUri = "http://localhost:5000";
            //     options.LowerCaseIssuerUri = true;

            // }).AddDeveloperSigningCredential()
            //     .AddAspNetIdentity<User>()
            //     .AddClientStore<ClientIdentityInfoQuery>()
            //     .AddResourceStore<ResourceQuery>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityServer", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger(GetType());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.Use(next => 
            {
                return async context =>
                {
                    var request = context.Request;
                    logger.LogInformation("Content-Type:{}", request.ContentType);
                    await next(context);
                };
            });

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
