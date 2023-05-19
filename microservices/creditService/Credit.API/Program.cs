using Credit.Application.Repositories;
using Credit.Application.UseCases;
using Credit.Infrastructure.Data;
using Credit.Infrastructure.RabbitMQ;
using Credit.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICreditRepository, CreditRepository>();
builder.Services.AddScoped<IAdimplenteRepository, AdimplenteRepository>();
builder.Services.AddScoped<IInadimplenteRepository, InadimplenteRepository>();
builder.Services.AddScoped<ILiberarCreditoUseCase, LiberarCreditoUseCase>();
builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ImplementPersistence(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
