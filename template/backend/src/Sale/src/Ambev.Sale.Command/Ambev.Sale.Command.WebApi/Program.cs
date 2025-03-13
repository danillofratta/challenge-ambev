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
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() 
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")     
    .CreateLogger();

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

//config  rebus, todo remove from here
builder.Services.AddRebus(configure => configure
                .Logging(l => l.Console())
#if DEBUG
                .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost", "SaleCommandEndPoint"))
#else
                .Transport(t => t.UseRabbitMq("amqp://guest:guest@rabbitmq", "SaleCommandEndPoint"))
#endif
                .Routing(r => r.TypeBased()                
                    .Map<SaleCreatedEvent>("SaleQueryEndPoint")
                    .Map<SaleUpdatedEvent>("SaleQueryEndPoint")
                    .Map<SaleDeletedEvent>("SaleQueryEndPoint")
                    .Map<SaleCanceledEvent>("SaleQueryEndPoint")
                    .Map<SaleItemCanceledEvent>("SaleQueryEndPoint")));

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

//config subscribers rebus, todo remove from here
using (var scope = app.Services.CreateScope())
{
    var bus = scope.ServiceProvider.GetRequiredService<IBus>();
    await bus.Subscribe<SaleCreatedEvent>();
    await bus.Subscribe<SaleUpdatedEvent>();
    await bus.Subscribe<SaleDeletedEvent>();
    await bus.Subscribe<SaleCanceledEvent>();
    await bus.Subscribe<SaleItemCanceledEvent>();
}

app.Run();
