using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Query.Consumer.Domain.Repository.Sale;
using Ambev.Sale.Query.Consumer.Domain.Repository.SaleItem;
using Ambev.Sale.Query.Consumer.Infrastructure.Orm.Repository.Sale;
using Ambev.Sale.Query.Consumer.Infrastructure.Orm.Repository.SaleItem;
using Ambev.Sale.Query.Consumer.WebApi.Sales;
using Ambev.Sale.Query.Consumer.WebApi.SalesItem;
using Rebus.Bus;
using Rebus.Config;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SaleQueryDbContext>();

builder.Services.AddScoped<ISaleCommandConsumerRepository, SaleCommandConsumerRepository>();
builder.Services.AddScoped<ISaleQueryConsumerRepository, SaleQueryConsumerRepository>();

builder.Services.AddScoped<ISaleItemCommandConsumerRepository, SaleItemCommandConsumerRepository>();
builder.Services.AddScoped<ISaleItemQueryConsumerRepository, SaleItemQueryConsumerRepository>();

builder.Services.AddRebus(configure => configure
    .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost", "SaleQueryEndPoint"))        
    .Logging(l => l.Trace()))
    .AutoRegisterHandlersFromAssemblyOf<SaleCanceledEventHandler>()
    .AutoRegisterHandlersFromAssemblyOf<SaleCreatedEventHandler>()
    .AutoRegisterHandlersFromAssemblyOf<SaleUpdatedEventHandler>()
    .AutoRegisterHandlersFromAssemblyOf<SaleDeletedEventHandler>()
    .AutoRegisterHandlersFromAssemblyOf<SaleItemCanceledEventHandler>();

//builder.Services.AutoRegisterHandlersFromAssembly(typeof(SaleCreatedEventHandler).Assembly);

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

