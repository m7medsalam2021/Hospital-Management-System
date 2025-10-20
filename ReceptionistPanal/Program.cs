using Hospital.Core.Entities.Identity;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using Hospital.Repository.Data;
using Hospital.Repository.Identity;
using Hospital.Repository.Repositories;
using Hospital.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HospitalContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//register AppIdentityDbContext
builder.Services.AddDbContext<ApplicationIdentityContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("identityConnection"));
});
builder.Services.AddScoped(typeof(IAppointmentService), typeof(AppointmentService));
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<ApplicationIdentityContext>();

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
    pattern: "{controller=Receptionist}/{action=Login}/{id?}");

app.Run();
