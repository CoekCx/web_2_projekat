using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewAvalon.App.Extensions;
using System.IO;

namespace NewAvalon.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services.InstallServicesFromAssembly(GetType().Assembly);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder.SetIsOriginAllowed(o => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            if (env.IsDevelopment() || env.IsEnvironment("Debug"))
            {
                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.Map("/swagger/v1/swagger.json", b => b.Run(async x =>
                {
                    string json = await File.ReadAllTextAsync("swagger.json");

                    await x.Response.WriteAsync(json);
                }));

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App"));

                app.ApplyMigrations();
            }

            app.UseResponseCompression();

            app.UseGlobalExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
