using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Contracts.Events;
using Ambev.Sale.Query.Consumer.Domain.Repository;
using Ambev.Sale.Query.Consumer.Infrastructure.Orm;
using Ambev.Sale.Query.Consumer.WebApi;
using PAmbev.Sale.Query.Consumer.WebApi;
using Rebus.Bus;
using Rebus.Config;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

//builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SaleQueryDbContext>();

builder.Services.AddScoped<ISaleCommandConsumerRepository, SaleCommandConsumerRepository>();
builder.Services.AddScoped<ISaleQueryConsumerRepository, SaleQueryConsumerRepository>();

builder.Services.AddRebus(configure => configure
    .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost", "SaleQueryEndPoint")))
    .AutoRegisterHandlersFromAssemblyOf<SaleCanceledEventHandler>()
    .AutoRegisterHandlersFromAssemblyOf<SaleCreatedEventHandler>()
    .AutoRegisterHandlersFromAssemblyOf<SaleUpdatedEventHandler>()
    .AutoRegisterHandlersFromAssemblyOf<SaleDeletedEventHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.AllowAnyOrigin()
);

app.UseCors((g) => g.AllowAnyOrigin());
app.UseCors((g) => g.AllowCredentials());

app.MapControllers();


app.Run();

