using Register.API.Data;
using Register.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddDbContext<DbContextClass>();
builder.Services.AddScoped<RabbitMQConsumer>(provider =>
{
    var dbContext = provider.GetRequiredService<DbContextClass>();
    return new RabbitMQConsumer("cadastroCliente", "cadastro", "cliente", dbContext);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
