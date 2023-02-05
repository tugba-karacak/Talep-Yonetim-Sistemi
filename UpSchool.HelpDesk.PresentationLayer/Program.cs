using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UpSchool.HelpDesk.PresentationLayer.ApiServices;
using UpSchool.HelpDesk.PresentationLayer.Helpers;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<TokenHelper>();

builder.Services.AddHttpClient<AuthApiService>(opt =>
{
    opt.BaseAddress = new Uri(ApiDefaults.BaseUrl);
});

builder.Services.AddHttpClient<WorkRequestApiService>(opt =>
{
    opt.BaseAddress = new Uri(ApiDefaults.BaseUrl);
});

builder.Services.AddHttpClient<UserApiService>(opt =>
{
    opt.BaseAddress = new Uri(ApiDefaults.BaseUrl);
});

builder.Services.AddHttpClient<WorkRequestStateApiService>(opt =>
{
    opt.BaseAddress = new Uri(ApiDefaults.BaseUrl);
});


builder.Services.AddAuthentication().AddCookie("default");


var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.SameAsRequest,
};



app.UseCookiePolicy(cookiePolicyOptions);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
