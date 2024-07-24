using Microsoft.AspNetCore.Authentication.Cookies;
using Progetto_Albergo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

builder.Services
    .AddScoped<IClienteService, ClienteService>()
     .AddScoped<ICameraService, CameraService>()
     .AddScoped<IAuthService, AuthService>()
      .AddScoped<IPrenotazioneService, PrenotazioneService>();




builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt => {
        // pagina alla quale l'utente sarà indirizzato se non è stato già riconosciuto
        opt.LoginPath = "/Account/Login";
    })
    ;

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
