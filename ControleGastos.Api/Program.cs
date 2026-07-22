using System.Text.Json.Serialization;
using ControleGastos.Api;
using ControleGastos.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos Controllers + Resolução de Ciclos no JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Configuração do Banco de Dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=gastos.db");
});

// Registro dos Serviços (Injeção de Dependência)
builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<TransacaoService>();
builder.Services.AddScoped<TotalService>();

// Configuração do CORS para integração com o React
builder.Services.AddCors(options =>
{
    options.AddPolicy("React", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Garante que o banco de dados e tabelas sejam criados automaticamente ao iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // Cria o banco SQLite e aplica as migrações se não existirem
}

app.UseCors("React");

app.MapControllers();

app.Run();