using Microsoft.OpenApi.Models;
using Vypex.CodingChallenge.Domain;
using Vypex.CodingChallenge.Infrastructure;
using Vypex.CodingChallenge.Application;
using Vypex.CodingChallenge.Infrastructure.Data;

namespace Vypex.CodingChallenge.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddApiModule()
                .AddDomainModule()
                .AddApplication()
                .AddInfrastructureModule(builder.Configuration.GetConnectionString("DefaultConnection") ??
                    throw new ArgumentException("Connection string not specified"));
            builder.Services
                .AddControllers()
                .AddApiControllers();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "v1",
                    Version = "v1"
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });


            var app = builder.Build();
            SeedData.Initialize(app.Services);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "openapi/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/openapi/v1/swagger.json", "v1");
                    c.RoutePrefix = "swagger";
                });

                app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/", () => "Vypex Coding Challenge service is running.");

            app.Run();
        }
    }
}
