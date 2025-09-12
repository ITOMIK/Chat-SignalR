using Chat_SignalR.Data;
using Chat_SignalR.Repositories;
using Chat_SignalR.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR().AddHubOptions<ChatHub>(options => options.EnableDetailedErrors = true);      // подключема сервисы SignalR


// 1. Подключаем настройки из appsettings.json
builder.Services.Configure<AuthOptions>(
    builder.Configuration.GetSection(AuthOptions.SectionName));

builder.Services.AddSingleton<JwtService>();
builder.Services.AddDbContext<DataBaseContext>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<UserReposytory>();
builder.Services.AddScoped<BreanchRepository>();
builder.Services.AddScoped<MessageRepository>();
builder.Services.AddScoped<AuthService>();
// 2. Регистрируем конфигуратор для JwtBearerOptions
builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtBearerOptionsConfigurator>();

// 3. Добавляем авторизацию и аутентификацию
builder.Services.AddAuthentication("JwtCookie")
    .AddScheme<AuthenticationSchemeOptions, JwtCookieAuthenticationHandler>("JwtCookie", options => { });
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// dev swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 401) // не авторизован
    {
        context.Response.Redirect("/Auth/Login");
    }
    else if (context.Response.StatusCode == 403) // нет доступа
    {
        context.Response.Redirect("/Home/AccessDenied");
    }
});


// Сначала аутентификация
app.UseAuthentication();

//потом авторизация
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chatHub");

app.Run();

