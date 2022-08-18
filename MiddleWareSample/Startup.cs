using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Infrastructure.Converters;
using Microsoft.IdentityModel.Tokens;
using MiddleWareSample.MiddleWares;
using MiddleWareSample.Extensions;

namespace MiddleWareSample
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

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Bearer";
                // options.DefaultChallengeScheme = "oidc";

            }).AddIdentityServerAuthentication("Bearer", options =>
            {
                options.SaveToken = true;
                options.Authority = "http://localhost:5000";
                options.ClaimsIssuer = "IdentityServer";
                options.RequireHttpsMetadata = false;

            }).AddJwtBearer("Jwt", options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {

                };
            });
            
            // .AddOpenIdConnect("oidc", options =>
            // {
            //     options.Authority = "http://localhost:5000";
            //     options.ClientId = "middleware-sample";
            //     options.ClientSecret = "middleware";
            //     options.SaveTokens = true;
            //     options.RequireHttpsMetadata = false;
            // });

            services.AddAuthorization();

            services.AddSingleton<ClientIpMiddleWare>();
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddleWareSample", Version = "v1" });
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddleWareSample v1"));
            }

            app.UseTest();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.Map("/test", app => app.Use(next => 
            {
                return async context =>
                {
                    logger.LogInformation("执行前");
                    var isForm = context.Request.HasFormContentType;

                    await next(context);

                    logger.LogInformation("执行后");
                };
            }));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
