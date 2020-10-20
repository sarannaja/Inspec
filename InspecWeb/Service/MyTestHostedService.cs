using System;
using System.Threading;
using System.Threading.Tasks;
using InspecWeb.Controllers;
using InspecWeb.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCrontab;

public class MyTestHostedService : BackgroundService
{
    private CrontabSchedule _schedule;
    private DateTime _nextRun;
    private UtinityController _utinityController;
    private string Schedule => "*/10 * * * * *"; //Runs every 10 seconds
    private readonly IServiceProvider _provider;

    public MyTestHostedService(UtinityController utinityController, IServiceProvider serviceProvider)
    {
        _utinityController = utinityController;
        _provider = serviceProvider;
        _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
        _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            var now = DateTime.Now;
            var nextrun = _schedule.GetNextOccurrence(now);

            using (IServiceScope scope = _provider.CreateScope())
            {
                if (now > _nextRun)
                {
                    Process();
                    // _utinityController.Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userList = context.Subjects.Find(1);
            }

            await Task.Delay(5000, stoppingToken); //5 seconds delay
        }
        while (!stoppingToken.IsCancellationRequested);
    }

    private void Process()
    {
        Console.WriteLine("hello world" + DateTime.Now.ToString("F"));
    }
}