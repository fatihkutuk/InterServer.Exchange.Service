using InterServer.Exchange.Service.Manager;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterServer.Exchange.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TagCloneManager tagCloneManager = new TagCloneManager();
            while (!stoppingToken.IsCancellationRequested)
            {              
               await tagCloneManager.Run();
               await Task.Delay(1000);
            }
        }
    }
}
