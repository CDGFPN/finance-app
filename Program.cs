using FinanceApp.Data;
using FinanceApp.Repositories;
using FinanceApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// === DEPENDENCY INJECTION ===
// Registra o DbContext (Scoped = uma instância por request HTTP)
builder.Services.AddDbContext<FinanceDbContext>();

// Registra Repositories (Scoped = uma instância por request)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Registra Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Adiciona suporte a Controllers
builder.Services.AddControllers();

// Adiciona Swagger (documentação da API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// === MIDDLEWARE PIPELINE ===
// Swagger só em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapeia os controllers para rotas
app.MapControllers();

app.Run();
