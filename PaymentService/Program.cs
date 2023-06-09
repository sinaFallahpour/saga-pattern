﻿using System.Reflection;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentService.Consumers;
using PaymentService.Services;

namespace PaymentService
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IPaymentService, Services.PaymentService>();

                    //var connectionString = hostContext.Configuration["RabbitMQ:ConnectionString"];
                    var connectionString = "host=localhost;username=guest;password=guest";
                    var bus = RabbitHutch.CreateBus(connectionString);

                    services.AddSingleton<IBus>(bus);
                    services.AddSingleton<MessageDispatcher>();
                    services.AddSingleton<AutoSubscriber>(_ =>
                    {
                        return new AutoSubscriber(_.GetRequiredService<IBus>(), Assembly.GetExecutingAssembly().GetName().Name)
                        {
                            AutoSubscriberMessageDispatcher = _.GetRequiredService<MessageDispatcher>()
                        };
                    });

                    services.AddScoped<StocksReservedEventConsumer>();

                    services.AddHostedService<Worker>();
                });
    }
}