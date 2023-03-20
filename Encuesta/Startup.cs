using Encuesta.Models;
using Google.Cloud.Language.V1;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSingleton<LanguageServiceClient>(s => new LanguageServiceClientBuilder
        {
            ChannelCredentials = ChannelCredentials.Insecure,
            Endpoint = "language.googleapis.com"
        }.Build());

        services.AddControllersWithViews();

        services.AddDbContext<MvccrudContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}

