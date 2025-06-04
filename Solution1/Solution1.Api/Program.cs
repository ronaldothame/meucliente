using Microsoft.EntityFrameworkCore;
using Solution1.Api.Middleware;
using Solution1.Domain.DTOs;
using Solution1.Domain.Interfaces;
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

builder.Services.AddScoped<IService<AtivoDto, CreateAtivoDto, UpdateAtivoDto>, AtivoService>();
builder.Services.AddScoped<IService<TipoAtivoDto, CreateTipoAtivoDto, UpdateTipoAtivoDto>, TipoAtivoService>();
builder.Services.AddScoped<IService<FornecedorDto, CreateFornecedorDto, UpdateFornecedorDto>, FornecedorService>();
builder.Services
    .AddScoped<IService<ContratoVendaDto, CreateContratoVendaDto, UpdateContratoVendaDto>, ContratoVendaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseErrorHandling();
app.UseAuthorization();
app.MapControllers();

app.Run();