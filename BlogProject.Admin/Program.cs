using BlogProject.Admin.Service;
using BlogProject.Apilntegration.Category;
using BlogProject.Apilntegration.Posts;
using BlogProject.Apilntegration.Roles;
using BlogProject.Apilntegration.Users;
using BlogProject.Application.Common;
using BlogProject.Data.EF;
using BlogProject.ViewModel.System.Users;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews()
 .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/Users/Forbidden/";
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
var connectionString = builder.Configuration.GetConnectionString("ConnectionStrings");
builder.Services.AddDbContext<BlogDbContext>(options =>
{
    // Chuỗi DataContext: Là chuỗi trong file Json: appsettings.Development (
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDbContext"));
});


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUserApiClient, UserApiClient>();
builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddTransient<IRoleApiClient, RoleApiClient>();
builder.Services.AddTransient<IPostApiClient, PostApiClient>();
builder.Services.AddTransient<ICategoryApiClient, CategoryApiClient>();

builder.Services.AddScoped<ICategoryApiClient, CategoryApiClient>();

IMvcBuilder mvcBuilder = builder.Services.AddRazorPages();
var envirment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (envirment == Environments.Development)
{
    mvcBuilder.AddRazorRuntimeCompilation();
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();