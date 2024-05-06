using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjetoEmprestimoLivros.Data;
using ProjetoEmprestimoLivros.Services.LivroService;
using ProjetoEmprestimoLivros.Services.UsuariosService;
using ProjetoEmprestimoLivros.Services.ViagemService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ILivroInterface, LivroService>(); //ILivroInterface implementa o LivroService
builder.Services.AddScoped<IViagemInterface, ViagemService>(); //IViagemInterface implementa o ViagemService
builder.Services.AddScoped<IUsuariosInterface, UsuariosService>(); 
builder.Services.AddAutoMapper(typeof(Program)); // Registra os profiles do AutoMapper baseados no assembly fornecido

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
    pattern: "{controller=Livro}/{action=Index}/{id?}");

app.Run();
