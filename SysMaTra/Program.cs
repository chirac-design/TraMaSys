using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SysMaTraContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SysMaTraContext") ?? throw new InvalidOperationException("Connection string 'SysMaTraContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SysMaTraContext>();


builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.HttpOnly = HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always;
});
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Login";
    //config.LoginPath = _configuration["Application:LoginPath"];
});
//builder.Services.AddMvc().AddRazorPagesOptions(Options => Options.AllowAreas = true);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<HttpClientHandler>();

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
