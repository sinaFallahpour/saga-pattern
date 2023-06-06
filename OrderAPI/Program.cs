using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using OrderAPI;
using OrderAPI.Consumers;
using OrderAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var bus = RabbitHutch.CreateBus(builder.Configuration["RabbitMQ:ConnectionString"]);
builder.Services.AddSingleton<IBus>(bus);
builder.Services.AddSingleton<MessageDispatcher>();

builder.Services.AddSingleton<AutoSubscriber>(_ =>
{
    return new AutoSubscriber(_.GetRequiredService<IBus>(), Assembly.GetExecutingAssembly().GetName().Name)
    {
        AutoSubscriberMessageDispatcher = _.GetRequiredService<MessageDispatcher>()
    };
});

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<PaymentCompletedEventConsumer>();
builder.Services.AddScoped<StocksReleasedEventConsumer>();

builder.Services.AddHostedService<Worker>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
