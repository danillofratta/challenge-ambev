using Ambev.Sale.Command.Application;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Command.Infrastructure.Orm.Repository;
using Ambev.Sale.Core.Domain.Repository;
using Rebus.Config;
using Rebus;
using Rebus.Routing.TypeBased;
using Ambev.Sale.Contracts.Events;
using Ambev.Base.Infrastructure.Messaging;
using Base.Infrastruture.Messaging.Rebus;
using Ambev.Sale.Core.Domain.Service;
using Rebus.Bus;
using Ambev.Sale.Contracts.Events.SaleItem;
using Rebus.Retry.Simple;

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

//TODO move to other project
builder.Services.AddRebus(configure => configure
                .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost", "SaleCommandEndPoint"))
                .Routing(r => r.TypeBased()
                    .Map<SaleCreatedEvent>("SaleQueryEndPoint")
                    .Map<SaleUpdatedEvent>("SaleQueryEndPoint")
                    .Map<SaleDeletedEvent>("SaleQueryEndPoint")
                    .Map<SaleCanceledEvent>("SaleQueryEndPoint")
                    .Map<SaleItemCanceledEvent>("SaleQueryEndPoint"))
                .Logging(l => l.Trace())
                .Options(o => o.SetNumberOfWorkers(1))
                .Options(o => o.SetMaxParallelism(1))
                .Options(o => o.RetryStrategy(errorQueueName: "errors", maxDeliveryAttempts: 5)));

builder.Services.AddScoped<IMessageBus, RebusAdapter>();
builder.Services.AddScoped<SaleDiscountService>();
builder.Services.AddScoped<SaleRecalculationService>();

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
