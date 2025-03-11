using Ambev.Sale.Command.Application;
using Ambev.Sale.Command.Infrastructure.Orm;
using Ambev.Sale.Command.Infrastructure.Orm.Repository;
using Ambev.Sale.Core.Domain.Repository;

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
