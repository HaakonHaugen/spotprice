using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SpotpPrice
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IStromPrisClient _stromPrisClient;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            
        }

        [Function("Function1")]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");


            var result = await _stromPrisClient.GetNordPoolPrisAsync();
            System.Console.WriteLine(result);


            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
