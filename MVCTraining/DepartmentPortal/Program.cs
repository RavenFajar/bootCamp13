using DepartmentPortal.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using DepartmentPortal.MappingProfiles;
using DepartmentPortal.Interfaces;
using DepartmentPortal.Services;
using DepartmentPortal.Models.Entities;
using DepartmentPortal.Validators;
using DepartmentPortal.Repositories;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrasi repository dan service
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IServiceDepartment, ServiceDepartment>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(MappingDepartment));
builder.Services.AddAutoMapper(typeof(MappingEmployee));
builder.Services.AddScoped<IServiceDepartment, ServiceDepartment>();
builder.Services.AddScoped<IServiceEmployee, ServiceEmployee>();

// Configure FluentValidation
ConfigureFluentValidation(builder.Services);

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

static void ConfigureFluentValidation(IServiceCollection services)
{
    // Add FluentValidation services
    services.AddFluentValidationAutoValidation(options =>
    {
        // Disable automatic validation for properties with [BindNever] attribute
        options.DisableDataAnnotationsValidation = false;
        
        // Configure implicit validation for child properties
        // options.ImplicitlyValidateChildProperties = true;
    });

    // Add client-side validation adapters for better user experience
    services.AddFluentValidationClientsideAdapters();

    // Register all validators from the current assembly
    services.AddValidatorsFromAssemblyContaining<DepartmentValidator>();

    // Alternative: Register validators individually for more control
    // services.AddScoped<IValidator<Student>, StudentValidator>();
    // services.AddScoped<IValidator<Grade>, GradeValidator>();
}