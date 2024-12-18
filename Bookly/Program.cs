using Microsoft.EntityFrameworkCore;
using Bookly.Data;
using Bookly.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Configure database with SQLite
builder.Services.AddDbContext<BooklyContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BooklyContext")
                      ?? throw new InvalidOperationException("Connection string 'BooklyContext' not found.")));

// Configure Identity with required account confirmation
builder.Services.AddDefaultIdentity<DefaultUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BooklyContext>();

builder.Services.AddTransient<IEmailSender, EmailSender>();


// Add services for controllers with views
builder.Services.AddControllersWithViews();

// Add Razor Pages
builder.Services.AddRazorPages();

// Add IHttpContextAccessor as a singleton
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Configure scoped service for Cart
builder.Services.AddScoped<Cart>(sp => Cart.getCart(sp));

// Configure session settings
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    // Uncomment if needed to set a timeout for sessions
    // options.IdleTimeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

// Configure the HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Enable session
app.UseSession();

// Define routing
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});


/*
// Seed roles and users
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await SeedRolesAndUsersAsync(serviceProvider);
}
app.MapControllers(); // If you're using controllers
*/


app.Run();

/*
// Ajouter la méthode SeedRolesAndUsersAsync ici
async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<DefaultUser>>();

    // Rôles à créer
    string[] roles = { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Ajouter un utilisateur admin par défaut
    var adminEmail = "admin@bookly.com";
    var adminPassword = "Admin@123";

    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new DefaultUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "User",
            Address = "Admin Street",
            City = "Admin City",
            ZipCode = "12345",
            UserCreationDate = DateTime.UtcNow
        };

        var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);

        if (createUserResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}*/
