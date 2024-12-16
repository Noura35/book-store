using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bookly.Data;
using Bookly.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BooklyContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BooklyContext") ?? throw new InvalidOperationException("Connection string 'BooklyContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



builder.Services.AddScoped<Cart>(sp => Cart.getCart(sp));

//session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    //options.IdleTimeout = TimeSpan.FromSeconds(10);
} );






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

//session:
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Store}/{action=Index}/{id?}");

app.Run();