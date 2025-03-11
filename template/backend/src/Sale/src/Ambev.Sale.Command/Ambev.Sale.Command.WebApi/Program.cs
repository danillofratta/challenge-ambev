using Ambev.Sale.Command.Application;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Command.Infrastructure.Orm.Repository;
using Ambev.Sale.Core.Domain.Repository;
using Rebus.Config;
using Rebus;
using Rebus.Routing.TypeBased;
using Ambev.Sale.Contracts.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(ApplicationLayer).Assembly,
        typeof(Program).Assembly
    );
});

builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);
builder.Services.AddDbContext<SaleCommandDbContext>();

builder.Services.AddScoped<ISaleCommandRepository, SaleCommandRepository>();
builder.Services.AddScoped<ISaleQueryRepository, SaleQueryRepository>();

builder.Services.AddScoped<ISaleItemCommandRepository, SaleItemCommandRepository>();
builder.Services.AddScoped<ISaleItemQueryRepository, SaleItemQueryRepository>();


//TODO add rebus
//builder.Services.AddMassTransit(x =>
//{
//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host(new Uri("amqp://guest:guest@localhost:5672/"));

//        cfg.ConfigureEndpoints(context);
//    });
//});

//builder.Services.AddMassTransitHostedService();

//todo move to other project
builder.Services.AddRebus(configure => configure
                .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost", "SaleSagaEndpoint"))
                .Routing(r => r.TypeBased().Map<SaleCreatedEvent>("queue-sale-created"))
                .Routing(r => r.TypeBased().Map<SaleUpdateEvent>("queue-sale-udpated"))
                .Routing(r => r.TypeBased().Map<SaleDeletedEvent>("queue-sale-deleted"))
                .Routing(r => r.TypeBased().Map<SaleCanceledEvent>("queue-sale-canceled")));


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
