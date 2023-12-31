using Laboratory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    //check
    var roles = new[] { "Admin", "Recep" };
    foreach (var role in roles)
    {
        var roleExist = await roleManager.RoleExistsAsync(role);
        if (!roleExist)
            await roleManager.CreateAsync(new IdentityRole(role));
    }


    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();



    var adminUser = await userManager.FindByEmailAsync("reem@gmail.com");
    if (adminUser == null)
    {
        var user = new IdentityUser
        {
            UserName = "reem@gmail.com",
            Email = "reem@gmail.com",
        };
        await userManager.CreateAsync(user, "A-123456a ");
        await userManager.AddToRoleAsync(user, "Admin");
    }

    var recepUser = await userManager.FindByEmailAsync("sara@gmail.com");
    if (recepUser == null)
    {
        var user = new IdentityUser
        {
            UserName = "sara@gmail.com",
            Email = "sara@gmail.com",
        };
        await userManager.CreateAsync(user, "A-123456a");
        await userManager.AddToRoleAsync(user, "Recep");
    }
}

app.Run();
