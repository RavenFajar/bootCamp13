using DepartmentPortal.Data;
using Microsoft.EntityFrameworkCore;
using DepartmentPortal.MappingProfiles;
using DepartmentPortal.Interfaces;
using DepartmentPortal.Services;
{
    
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(MappingDepartment));
builder.Services.AddAutoMapper(typeof(MappingEmployee));
builder.Services.AddScoped<IServiceDepartment, ServiceDepartment>();
builder.Services.AddScoped<IServiceEmployee, ServiceEmployee>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
