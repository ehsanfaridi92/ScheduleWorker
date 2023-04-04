using Quartz;
using Schedule.Worker.Services;

namespace Schedule.Worker.Jobs;

public class SynchronizerJob : IJob
{
    private readonly IScheduleService _scheduleService;

    public SynchronizerJob(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await _scheduleService.DoSomething();
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.Message);
        }
    }
}

