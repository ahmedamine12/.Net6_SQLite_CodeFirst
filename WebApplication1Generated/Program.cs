using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;
using WebApplication1Generated.Controllers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(options => 
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<AppContextDB>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AppContextDB") ?? throw new InvalidOperationException("Connection string 'AppContextDB' not found.")));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ProduitController>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();  // Add this line to enable session

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produit}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
