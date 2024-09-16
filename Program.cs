using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjetoEmprestimoLivros.Data;
using ProjetoEmprestimoLivros.Services.AutenticacaoService;
using ProjetoEmprestimoLivros.Services.LivroService;
using ProjetoEmprestimoLivros.Services.SessaoService;
using ProjetoEmprestimoLivros.Services.UsuariosService;
using ProjetoEmprestimoLivros.Services.HomeService;
using ProjetoEmprestimoLivros.Services.EmprestimoService;
using ProjetoEmprestimoLivros.Services.RelatorioService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization(); //Permite comparar propriedades de um modelo com base em atributos de dados
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Adiciona o IHttpContextAccessor como um serviço singleton
builder.Services.AddScoped<ILivroInterface, LivroService>(); //ILivroInterface implementa o LivroService
builder.Services.AddScoped<IUsuariosInterface, UsuariosService>(); 
builder.Services.AddScoped<IAutenticacaoInterface, AutenticacaoService>(); 
builder.Services.AddScoped<ISessaoInterface, SessaoService>(); 
builder.Services.AddScoped<IHomeInterface, HomeService>(); 
builder.Services.AddScoped<IEmprestimoInterface, EmprestimoService>(); 
builder.Services.AddScoped<IRelatorioInterface, RelatorioService>(); 

builder.Services.AddAutoMapper(typeof(Program)); // Registra os profiles do AutoMapper baseados no assembly fornecido

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
