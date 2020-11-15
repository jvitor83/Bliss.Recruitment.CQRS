using Bliss.Recruitment.Api.Exceptions;
using Bliss.Recruitment.API.Configuration;
using Bliss.Recruitment.API.Framework;
using Bliss.Recruitment.Application.Configuration;
using Bliss.Recruitment.Application.Configuration.Emails;
using Bliss.Recruitment.Application.Configuration.Validation;
using Bliss.Recruitment.Domain.Framework;
using Bliss.Recruitment.Infrastructure;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bliss.Recruitment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _logger = ConfigureLogger();
            _logger.Information("Logger configured");


            Configuration = configuration;
        }

        private const string QuestionsConnectionString = "db";

        private static Serilog.ILogger _logger;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSwaggerDocumentation();

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });



            
            // TODO: Refactor - move to specific class (extension method)
            services.AddHealthChecks()
                .AddSqlServer(connectionString: this.Configuration.GetConnectionString(QuestionsConnectionString),
                name: "sqlserver",
                failureStatus: HealthStatus.Unhealthy,
                timeout: TimeSpan.FromSeconds(5));


            services.AddHttpContextAccessor();
            var serviceProvider = services.BuildServiceProvider();

            IExecutionContextAccessor executionContextAccessor = new ExecutionContextAccessor(serviceProvider.GetService<IHttpContextAccessor>());

            var emailsSettings = Configuration.GetSection("EmailsSettings").Get<EmailsSettings>();

            return ApplicationStartup.Initialize(
                services,
                this.Configuration.GetConnectionString(QuestionsConnectionString),
                null,
                emailsSettings,
                _logger,
                executionContextAccessor);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CorrelationMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseProblemDetails();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            
            // TODO: Refactor - move to specific class (extension method)
            app.UseHealthChecks("/health",
               new HealthCheckOptions
               {
                   ResponseWriter = async (context, report) =>
                   {
                       var result = System.Text.Json.JsonSerializer.Serialize(
                           new
                           {
                               status = HelthCheckStatusToString(report.Status),
                           });
                       context.Response.ContentType = MediaTypeNames.Application.Json;
                       await context.Response.WriteAsync(result);
                   }
               });


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerDocumentation();

        }

        private static string HelthCheckStatusToString(HealthStatus status)
        {
            switch (status)
            {
                case HealthStatus.Unhealthy:
                    return "Service Unavailable. Please try again later.";
                    break;
                case HealthStatus.Degraded:
                    return "Service unstable, proceed with caution.";
                    break;
                case HealthStatus.Healthy:
                    return "OK";
                    break;
                default:
                    return "Unknown";
                    break;
            }
        }

        private static Serilog.ILogger ConfigureLogger()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.RollingFile(new CompactJsonFormatter(), "logs/logs")
                .CreateLogger();
        }

    }
}
