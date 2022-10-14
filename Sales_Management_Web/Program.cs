using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Sales_Management_Web;
using Sales_Management_Web.Services;
using Sales_Management_Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddHttpClient<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddHttpClient<IServicesService, ServicesService>();
builder.Services.AddScoped<IServicesService, ServicesService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddHttpClient<IClientService, ClientService>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddHttpClient<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    option =>
    {
        option.Cookie.HttpOnly = true;
        option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        option.LoginPath = "/Auth/Login";
        option.AccessDeniedPath = "/Auth/AccessDenied";
        option.SlidingExpiration = true;
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
