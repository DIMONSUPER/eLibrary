using eLibrary.Data;
using eLibrary.Data.Repository;
using eLibrary.Helpers;
using Microsoft.EntityFrameworkCore;

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

app.UseCors(options => options
    .WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .WithExposedHeaders("*")
    .AllowAnyMethod()
    .AllowCredentials());

app.MapDefaultControllerRoute();

app.MapFallbackToFile("index.html");
app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddCors();
    services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
    services.AddControllersWithViews()
        .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    services.AddScoped<IRepository, Repository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IAuthorRepository, AuthorRepository>();
    services.AddScoped<IBookRepository, BookRepository>();
    services.AddScoped<IJwtService, JwtService>();
}