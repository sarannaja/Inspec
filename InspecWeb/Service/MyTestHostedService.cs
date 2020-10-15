using System;
using System.Threading;
using System.Threading.Tasks;
using InspecWeb.Controllers;
using Microsoft.Extensions.Hosting;
using NCrontab;

public class CronJobService : BackgroundService
{
    private CrontabSchedule _schedule;
    private DateTime _nextRun;
    private UtinityController _utinityController;
    private string Schedule => "*/10 * * * * *"; //Runs every 10 seconds

    public CronJobService(UtinityController utinityController)
    {
        _utinityController = utinityController;
        _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
        _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            var now = DateTime.Now;
            var nextrun = _schedule.GetNextOccurrence(now);
            if (now > _nextRun)
            {
                InitControllers();
                _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            }
            await Task.Delay(5000, stoppingToken); //5 seconds delay
        }
        while (!stoppingToken.IsCancellationRequested);
    }

    private void InitControllers()
    {
        // We can't set this at Ctor because we don't have our local copy yet
        // Access to Url 
        _utinityController.Process();
        // For more references see https://github.com/aspnet/Mvc/blob/6.0.0-rc1/src/Microsoft.AspNet.Mvc.ViewFeatures/Controller.cs
        // Note: This will change in RC2
    }


}