using InvestmentWallet.Domain.Interfaces.Repositories;
using InvestmentWallet.Domain.Interfaces.Services;
using InvestmentWallet.Domain.Services;
using InvestmentWallet.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Working with sessions
builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromSeconds(10);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});
// end config working with sessions


// Ler a connection string do banco de dados
string connectionString = builder.Configuration.GetConnectionString("BDInvestmentWallet");

// Injeção de dependência da connectionString
builder.Services.AddTransient<IUsuarioRepository>(map => new UsuarioRepository(connectionString));
builder.Services.AddTransient<IPerfilInvestidorRepository>(map => new PerfilInvestidorRepository(connectionString));
builder.Services.AddTransient<ICarteiraRepository>(map => new CarteiraRepository(connectionString));
builder.Services.AddTransient<IOperacaoRepository>(map => new OperacaoRepository(connectionString));
builder.Services.AddTransient<ITipoAtivoRepository>(map => new TipoAtivoRepository(connectionString));
builder.Services.AddTransient<ITipoOperacaoRepository>(map => new TipoOperacaoRepository(connectionString));
builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
builder.Services.AddTransient<IOperacaoDomainService, OperacaoDomainService>();
builder.Services.AddTransient<ICarteiraDomainService, CarteiraDomainService>();


// Habilitando o projeto para usar cookies e autenticação de acesso
builder.Services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options => {
        //     Controls how much time the authentication ticket stored in the cookie will remain
        //     valid from the point it is created The expiration information is stored in the
        //     protected cookie ticket.
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        //     The SlidingExpiration is set to true to instruct the handler to re-issue a new
        //     cookie with a new expiration time any time it processes a request which is more
        //     than halfway through the expiration window.
        options.SlidingExpiration = true;
        //     The AccessDeniedPath property is used by the handler for the redirection target
        //     when handling ForbidAsync.
        options.AccessDeniedPath = "/Forbidden/";
    }
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

//autenticação e autorização
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}"
);

//app.MapControllerRoute(
//    name: "dashboard",
//    pattern: "{controller=Dashboard}/{action=Index}"
//);

app.Run();
