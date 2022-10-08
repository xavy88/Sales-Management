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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
