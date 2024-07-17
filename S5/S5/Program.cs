using Microsoft.AspNetCore.Authentication.Cookies;
using S5.Nuova_cartella1;
using S5.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddScoped<IClienteService, ClienteService>()
    .AddScoped<ISpedizioneService, SpedizioneService>()
    .AddScoped<IAggiornamentoSpedizioneService, AggiornamentoSpedizioneService>()
    .AddScoped<IAuthService, AuthService>()
    .AddControllersWithViews();
    
// configurazione dell'autenticazione
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt => {
        // pagina alla quale l'utente sarà indirizzato se non è stato già riconosciuto
        opt.LoginPath = "/Account/Login";
    })
    ;
// fine configurazione dell'autenticazione



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
