using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 
using DataAccess;
using Infrastructure.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<UnitofWork>();
builder.Services.AddScoped<DbInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
SeedDatabase();
void SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    dbInitializer.Initialize();
}

License.LicenseKey = "IRONSUITE.NSTEPHENSON.MAIL.WEBER.EDU.7844-B43FF56EFF-BRATJE3RMGRN5CLG-EEVDJHHDUSZB-LKYE3AZQAZPR-PDPS7RMI7G4Q-KNZAUZYDDICA-OVPBMP343SCT-SED3KC-T6TLNIUXN76MEA-DEPLOYMENT.TRIAL-XZRP5W.TRIAL.EXPIRES.18.MAY.2024";

app.Run();
