using Application.ConcertTickets;
using EasyArchitect.OutsideApiControllerBase;
using EasyArchitect.OutsideManaged.AuthExtensions.Models;
using EasyArchitect.OutsideManaged.AuthExtensions.Services;
using EasyArchitect.OutsideManaged.Configuration;
using EasyArchitect.OutsideManaged.JWTAuthMiddlewares;
using Infrastructure.ConcertTickets;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Mxic.FrameworkCore.Core;
using System.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;
using Web.XConcertTickets.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllersWithViews()
    //.ConfigureApiBehaviorOptions(setup =>
    //{
    //    // 關閉驗證失敗時會自動回應 HTTP 400
    //    setup.SuppressModelStateInvalidFilter = true;
    //})
    ;

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(configure =>
{
    configure.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/Account/Login");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.Cookie.HttpOnly = true;
    options.Events = new CookieAuthenticationEvents()
    {
        OnRedirectToReturnUrl = async (context) =>
        {
            context.HttpContext.Response.Cookies.Delete(AuthenticateUser.LOGIN_USER_INFO);
        }
    };
});

// 註冊 AppSettings Configuration 類型，可在類別中注入 IOptions<AppSettings>
IConfigurationSection appSettingRoot = builder.Configuration.GetSection("AppSettings");

builder.Services.Configure<AppSettings>(appSettingRoot);

builder.Services.AddDbContext<ModelContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OutsideDbContext"), oraOptions => oraOptions.UseOracleSQLCompatibility("11"));
});

builder.Services.AddScoped<ModelContext>();

builder.Services.AddScoped<IUserService, UserService>(x => new UserService(
    new AppSettings()
    {
        Secret = appSettingRoot.GetSection("Secret").Value,
        TimeoutMinutes = Convert.ToInt32(appSettingRoot.GetSection("TimeoutMinutes").Value)
    }, x.GetRequiredService<ModelContext>()));

builder.Services.AddScoped<IUriExtensions, UriExtensions>();
builder.Services.AddScoped<IReserveRepository, ReserveRepository>();
builder.Services.AddScoped<ConcertTicketAppService>(c => new ConcertTicketAppService(c.GetRequiredService<IReserveRepository>()));

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

app.UseRouting();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();
app.UseJwtAuthenticate();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
});

app.Run();
