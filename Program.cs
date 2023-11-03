using System.Security.Policy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OptiApp.Models;
using OptiApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
await builder.Services.CreateRolesAndDefaultAdmin(builder.Configuration["DefaultAdmin:Username"], builder.Configuration["DefaultAdmin:Password"]);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "Identity/Pages/Account/Login"; // Verify that this path is correct
        options.AccessDeniedPath = "Identity/Pages/Account/AccessDenied";
    });
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddRazorPages();
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
app.UseAuthentication();
app.UseAuthorization();
// app.Use(async (context, next) =>
// {
//     if (context.User.Identity!.IsAuthenticated)
//     {
//         var userManager = context.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
//         var user = await userManager.GetUserAsync(context.User);
//
//         if (user != null)
//         {
//             var roles = await userManager.GetRolesAsync(user);
//
//             if (roles.Contains(Roles.Admin.ToString()))
//             {
//                 var url = $"{context.Request.Scheme}://{context.Request.Host}/Admin";
//                 context.Response.Redirect(url);
//             }
//
//             if (roles.Contains(Roles.Optometrist.ToString()))
//             {
//                 var url = $"{context.Request.Scheme}://{context.Request.Host}/Opt/";
//                 context.Response.Redirect(url);
//             }
//             context.Response.Redirect("/Home");
//         }
//     }
//     await next();
// });
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

await app.RunAsync();
