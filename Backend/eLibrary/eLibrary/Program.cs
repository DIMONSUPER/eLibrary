using BGNet.TestAssignment.Api.Data;
using BGNet.TestAssignment.Api.Data.Repository;
using BGNet.TestAssignment.DataAccess.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(options => options
    .WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .WithExposedHeaders("*")
    .AllowAnyMethod()
    .AllowCredentials());

app.MapDefaultControllerRoute();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.Configure<JwtOptions>(
        builder.Configuration.GetSection(JwtOptions.Jwt));

    services.AddCors();

    services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

    services.AddControllersWithViews()
        .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    services.AddScoped<IRepository, Repository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IAuthorRepository, AuthorRepository>();
    services.AddScoped<IBookRepository, BookRepository>();

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtOptions = builder.Configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>()!;

        options.Authority = jwtOptions.Authority;

        options.TokenValidationParameters =
          new TokenValidationParameters
          {
              ValidateAudience = false,
              ValidateLifetime = true,
              ValidateIssuer = true,
              ValidIssuer = jwtOptions.Issuer,
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.SecureKey)),
          };

           options.RequireHttpsMetadata = false;
    });
}