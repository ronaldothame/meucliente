using Microsoft.EntityFrameworkCore;
using Solution1.Domain.Interfaces;
using Solution1.Domain.Services;
using Solution1.Infra.Data;
using Solution1.Infra.Repositories;
using Solution1.Infra.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<ITipoAtivoService, TipoAtivoService>();
builder.Services.AddScoped<IAtivoService, AtivoService>();
builder.Services.AddScoped<IContratoVendaService, ContratoVendaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();