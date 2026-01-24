using ProjetoCRM.Repository;
using ProjetoCRM.Application;
using ProjetoCRM.Repository.Context;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Adicione serviços ao contêiner.
builder.Services.AddScoped<IUserApplication, UserApplication>();

// Adicione as interfaces de banco de dados
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Adiciona os serviços

builder.Services.AddControllers();

// Adicione o serviço de banco de dados
builder.Services.AddDbContext<ProjetoCRMContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Saiba mais sobre como configurar o Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
