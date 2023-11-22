using BGNet.TestAssignment.BusinessLogic;
using BGNet.TestAssignment.DatabaseMigrator;
using BGNet.TestAssignment.Common;
using Microsoft.EntityFrameworkCore;
using BGNet.TestAssignment.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BGNet.TestAssignment.Common.Configurations.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        services.Configure<JwtOptions>(_configuration.GetSection(JwtOptions.Jwt));

        services.AddCors();

        services.AddMigrations(_configuration.GetConnectionString("Default"));

        services.AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        services.RegisterRepository();

        services.RegisterServicesAndValidators();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtOptions = _configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>();

            if (jwtOptions is not null)
            {
                options.Authority = jwtOptions.Authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.SecureKey)),
                };
            }

            options.RequireHttpsMetadata = false;
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
}
