using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcWebIdentity.Context;
using MvcWebIdentity.Policies;
using MvcWebIdentity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "AspNetCore.Cookies";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.SlidingExpiration = true;
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 10;
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequireNonAlphanumeric = false;
});
builder.Services.AddScoped<IAuthorizationHandler, TempoCadastroHandler>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<ISeedUserClaimsInitial, SeedUserClaimsInitial>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireUserAdminGerenteRole", policy => policy.RequireRole("User", "Admin", "Gerente"));
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SomenteFuncionario", policy => policy.RequireClaim("FuncionarioNumero"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdminClaimAccess", policy => policy.RequireClaim("IsAdmin", "true"));
    options.AddPolicy("IsAdminClaimAccess", policy => policy.RequireClaim("CadastradoEm"));
    options.AddPolicy("IsFuncionarioClaimAccess", policy => policy.RequireClaim("IsFuncionario", "true"));

    options.AddPolicy("TempoCadastroMinimo", policy => policy.Requirements.Add(new TempoCadastroRequirement(5)));
});


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
await CriarPerfisUsuariosAsync(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "MinhaArea",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


async Task CriarPerfisUsuariosAsync(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        //var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        //await service.SeedRolesAsync();
        //await service.SeedUsersAsync();

        var service = scope.ServiceProvider.GetService<ISeedUserClaimsInitial>();
        await service.SeedUserClaims();

    }
} 