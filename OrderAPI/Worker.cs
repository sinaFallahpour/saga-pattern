using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.Hosting;

namespace OrderAPI
{
    public class Worker : BackgroundService
    {
        private readonly AutoSubscriber _autoSubscriber;
        public Worker(AutoSubscriber autoSubscriber)
        {
            _autoSubscriber = autoSubscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("worker called in OrderAPI");
            await _autoSubscriber.SubscribeAsync(new Assembly[] { Assembly.GetExecutingAssembly() }, stoppingToken);
        }
    }
}