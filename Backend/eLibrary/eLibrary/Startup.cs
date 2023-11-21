using BGNet.TestAssignment.BusinessLogic;
using BGNet.TestAssignment.DatabaseMigrator;
using BGNet.TestAssignment.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using BGNet.TestAssignment.DataAccess;

namespace BGNet.TestAssignment.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #region -- Public helpers --

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();

        services.AddMigrations(_configuration.GetConnectionString("Default"));

        services.AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        services.RegisterRepository();

        services.RegisterServicesAndValidators();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;
                options.Events.OnRedirectToLogin = OnRedirectToLogin;
            });
    } 

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseExceptionHandlerMiddleware();

        app.UseCors(options => options
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .WithExposedHeaders("*")
            .AllowAnyMethod()
            .AllowCredentials());

        app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
    }

    #endregion

    #region -- Private helpers --

    private Task OnRedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        return context.Response.WriteAsync("You should be authorized to view this page");
    }

    #endregion
}
