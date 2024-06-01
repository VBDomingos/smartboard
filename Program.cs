using Dashboard.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IPessoaRepository, PessoaRepository>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddTransient<ITecnicoRepository, TecnicoRepository>();
builder.Services.AddTransient<IAmbienteRepository, AmbienteRepository>();
builder.Services.AddTransient<IDispositivoRepository, DispositivoRepository>();
builder.Services.AddTransient<IRelatorioRepository, RelatorioRepository>();
builder.Services.AddTransient<IGrupoAmbienteRepository, GrupoAmbienteRepository>();
builder.Services.AddTransient<IAmbienteGrupoRepository, AmbienteGrupoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Dashboard/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
